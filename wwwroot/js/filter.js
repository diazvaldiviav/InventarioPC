let ButtonSearchi = document.querySelector(".buttonSearch");


ButtonSearchi.addEventListener("click", () => {
    let Tabla = document.querySelector("#tabla")
    const CuerpoTabla = document.querySelector("#CuerpoTabla")
    let filas = document.querySelectorAll(".fila");
    let ArrFilas = [...filas];
    let searchvalor = document.querySelector("#search").value;
    CuerpoTabla.remove();
    let CuerpoTabla2 = document.createElement("tbody")
    //recorrer el  array de filas con un for
    for (let i = 0; i < ArrFilas.length; i++) {

        if (ArrFilas[i].textContent.includes(searchvalor)) {
            console.log(ArrFilas);
            CuerpoTabla2.append(ArrFilas[i])
            Tabla.append(CuerpoTabla2);
        }



    }




});










