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
        console.log(json);
        const opciones = json.map(cargo => `<option value=${cargo.value}>
        ${cargo.text}</option>`);
        $("#CargoId").html(opciones);
})
}