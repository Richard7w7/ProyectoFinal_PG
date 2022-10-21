$(function () {
    var opcion = document.getElementById("viewBagCargo").value;
    switch (opcion) {
        case 'NO':
            Swal.fire({

                title: "El cargo no fue agregado",
                text: "comunicate con Tecnologia para informar sobre el error",
                icon: "warning",
            });
            break;
        case 'SI':
            Swal.fire({
                title: "Listo",
                text: "El cargo fue agregado",
                icon: "success",
            });
            break;
        default:
            break;
    }
});