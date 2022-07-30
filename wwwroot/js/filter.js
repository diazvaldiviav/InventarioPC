let ButtonSearchi = document.querySelector(".buttonSearch");
let boton = document.querySelector("#tablaImpr")

ButtonSearchi.addEventListener("click", buscarPc);

function buscarPc() {
    let Tabla = document.querySelector("#tabla")
    const CuerpoTabla = document.querySelector("#CuerpoTablaPc")
    let filas = document.querySelectorAll(".filapc");
    let ArrFilas = [...filas];
    let searchvalor = document.querySelector("#search").value;
    CuerpoTabla.remove();
    let CuerpoTabla2 = document.createElement("tbody")
    //recorrer el  array de filas con un for
    for (let i = 0; i < ArrFilas.length; i++) {
        if (ArrFilas[i].textContent.includes(searchvalor)) {
            CuerpoTabla2.append(ArrFilas[i])
            Tabla.append(CuerpoTabla2);
        }
    }

}



boton.addEventListener("mouseover", ()=>{
    alert("holaaaaa")
})



    











