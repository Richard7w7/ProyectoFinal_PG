$(function () {
    var opcion = document.getElementById("viewBagCodigo").value;
    switch (opcion) {
        case 'NO':
            Swal.fire({

                title: "El código no fue agregado",
                text: "comunicate con Tecnologia para informar sobre el error",
                icon: "warning",
            });
            break;
        case 'SI':
            Swal.fire({
                title: "Listo",
                text: "El código fue agregado",
                icon: "success",
            });
            break;
        default:
            break;
    }
});
