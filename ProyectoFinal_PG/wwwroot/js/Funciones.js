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
    if (tipoSolicitud.value == 3) {
        SolicitudPeriodoVacas.style.display = "block";
    } else {
        SolicitudPeriodoVacas.style.display = "none";
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



