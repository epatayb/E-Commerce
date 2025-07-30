const productConnection = new signalR.HubConnectionBuilder()
        .withUrl("/productHub")
        .build();

    productConnection.start().then(() => {
        // Bağlantı çalışıyorsa loglanır.
        console.log("🟢 ProductHub bağlantısı kuruldu."); })
        .catch((err) => {console.error(err.toString())
    });

    productConnection.on("ReceiveProductUpdate", function (product) {

        //Fiyat güncelleme
        const priceElement = document.querySelector(`#product-price-${product.id}`);
        if (priceElement != null) {
            priceElement.textContent = product.price.toFixed(2) + " ₺";

            window.refreshCartSummary();
        }

        //Stok güncelleme
        const stockElement = document.querySelector(`#product-stock-${product.id}`)
        if(stockElement) {
            let newHtml = "";

            if (product.stock <= 0) {
                newHtml = `
                <span class="badge bg-danger">
                    <i class="bi bi-x-circle-fill"> En kısa sürede tekrar stokta... </i>
                </span>`;
            } else if (product.stock <= 10) {
                newHtml = 
                `<span class="badge bg-warning text-dark">
                    <i class="bi bi-exclamation-triangle-fill"> Tükenmek üzere ! </i>
                </span>`;
            } 
            else {
                newHtml = 
                `<span class="badge bg-success">
                    <i class="bi bi-check-circle-fill"> Stokta </i>
                </span>`;
            }
            stockElement.innerHTML = newHtml;
        }

        // ürün adı güncelleme
        const nameElement = document.querySelector(`#product-name-${product.id}`);
        if (nameElement) {
            nameElement.textContent = product.productName; 
        }

        // ürün açıklama güncelleme
        const descriptionElement = document.querySelector(`#product-description-${product.id}`)
        if (descriptionElement) {
            descriptionElement.textContent = product.description;
        }

    });