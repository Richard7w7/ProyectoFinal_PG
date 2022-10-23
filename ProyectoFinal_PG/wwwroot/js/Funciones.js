$(document).ready(function () {
    $('#SolicitudFechasSeleccionadas').datepick(
        {
            multiSelect: 35,
            onDate: $.datepick.noWeekends
            
        },
        $.datepick.regionalOptions['es']);
});
function MuestraPeriodo() {
    var tipoSolicitud = document.getElementById("TiposolicitudId");
    var SolicitudPeriodoVacas = document.getElementById("conteneSolicitudPeriodoVacas");
    var SolicitudPeriodo = document.getElementById("SolicitudPeriodoVacas");
    var periodo = document.getElementById("viewBagPeriodo");
    console.log(periodo.value);
    if (tipoSolicitud.value == 3) {

        SolicitudPeriodo.value = periodo.value;
    } else {

        SolicitudPeriodo.value = null;
    }
}

function inicializarFormularioRegistro(urlObtenerCargos) {
    $("#DeptoId").change(async function () {
        const valorSeleccionado = $(this).val();
        const respuesta = await fetch(urlObtenerCargos, {
            method: 'POST',
            body: valorSeleccionado,
            headers: {
                'Content-Type': 'application/json'
            }
        });
        const json = await respuesta.json();
        
        const opciones = json.map(cargo => `<option value=${cargo.value}>
        ${cargo.text}</option>`);
        $("#CargoId").html(opciones);
})
}

$(function () {
    var opcion = document.getElementById("viewBagLimiteDias").value;
    switch (opcion) {
        case 'limite':
            Swal.fire({

                title: "Limite de días alcanzado",
                text: "la cantidad de días seleccionados supera a la cantidad de días disponibles",
                icon: "warning",
            });
            break;
        case 'creada':
            Swal.fire({
                title: "Listo",
                text: "tu solicitud ha sido creada",
                icon: "success",
            });
            break;
        case 'SinPeriodo':
            Swal.fire({
                title: "Ya no tienes periodos",
                text: "Lo sentimos ya no puedes crear solicitudes del tipo vacacional ya que te has ",
                icon: "warning",
            });
            break;
        default:
            break;
    }

    
});

