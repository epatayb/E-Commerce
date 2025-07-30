
const cartHubConnection = new signalR.HubConnectionBuilder()
        .withUrl("/cartHub")
        .build();

cartHubConnection.on("CartUpdated", function (userId)
{
    const currentUserId = document.querySelector('meta[name="user-id"]')?.content;
    if (!currentUserId || currentUserId === userId) {
        refreshCartSummary();
    }
});

cartHubConnection.on("QuantityChanged", function (productId, quantity) {
    const inputs = document.querySelectorAll(`.quantity-input[data-product-id="${productId}"]`);
    inputs.forEach(input => {
        input.value = quantity;
    });
});

cartHubConnection.start().then(() => {
        console.log("🟢 CartHub bağlantısı kuruldu."); })
        .catch((err) => {console.error(err.toString())
});

function updateQuantity(productId, quantity)
{
    fetch('/Cart/UpdateQuantity', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
        },
        body: JSON.stringify({ productId, quantity})
    })
    .then(res => res.json())
    .then(data => {   
        if (data.success) {
            document.getElementById("cart-count").innerText = data.itemCount;
            
            cartHubConnection.invoke("UpdateQuantity", data.userId);
            cartHubConnection.invoke("NotifyQuantityChanged", data.userId, data.updatedProductId, data.updatedQuantity);
            console.log("➡ NotifyQuantityChange gönderildi", data.userId, data.updatedProductId, data.updatedQuantity);
        }else {
            alert(data.message);
        }
    });
}

 window.refreshCartSummary = function() {
    fetch('/Cart/GetCartSummary')
        .then(res => res.json())
        .then(data => {
            document.getElementById('subtotal').textContent = data.subTotal + " ₺";
            document.getElementById('tax').textContent = data.tax + " ₺";
            document.getElementById('shipping-fee').textContent = data.shippingFee != 0 ? data.shippingFee + " ₺" : "Ücretsiz";
            document.getElementById('total').textContent = data.total + " ₺";
        });
}

document.querySelectorAll('.quantity-input').forEach(input => {
    input.addEventListener('change', function() {
        const productId = this.dataset.productId;
        const quantity = this.value;
        updateQuantity(productId, quantity);
    });
});