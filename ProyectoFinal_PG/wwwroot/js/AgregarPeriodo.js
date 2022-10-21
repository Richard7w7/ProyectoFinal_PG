$(function () {
    var opcion = document.getElementById("viewBagPeriodo").value;
    switch (opcion) {
        case 'NO':
            Swal.fire({

                title: "El periodo no fue agregado",
                text: "comunicate con Tecnologia para informar sobre el error",
                icon: "warning",
            });
            break;
        case 'SI':
            Swal.fire({
                title: "Listo",
                text: "El periodo fue agregado",
                icon: "success",
            });
            break;
        default:
            break;
    }
});