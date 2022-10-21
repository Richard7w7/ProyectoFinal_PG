$(function () {
    var opcion = document.getElementById("viewBagDepartamento").value;
    switch (opcion) {
        case 'NO':
            Swal.fire({

                title: "El departamento no fue agregado",
                text: "comunicate con Tecnologia para informar sobre el error",
                icon: "warning",
            });
            break;
        case 'SI':
            Swal.fire({
                title: "Listo",
                text: "El departamento fue agregado",
                icon: "success",
            });
            break;
        default:
            break;
    }
});


