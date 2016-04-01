function bounce() {
    $("#bouncy").effect("bounce", {}, 5000, null);

};
var couter = 0;
function alertAgain(nuevoMensaje) {
    var alerta = confirm(nuevoMensaje);
    if (couter == 5) {
        alert("Revisa tu consola...")
        console.log("Hola profe!")
    } else {
        if (alerta == true) {
            couter++;
            alertAgain("Te lo dije!");
        } else {
            couter++;
            alertAgain("Bueno, ni en cancelar...");
        }
    }
}