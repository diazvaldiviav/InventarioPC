let ButtonSearchi = document.querySelector(".buttonSearch");

ButtonSearchi.addEventListener("click", buscarPc);
console.log("script desde filtro");

function ocultandoFila(){
  let filas = document.querySelectorAll(".filapc");
  let ArrFilas = [...filas];

  ArrFilas.forEach((fila) => {
    if (fila.textContent.includes("Sin")) {
      fila.classList.add("inactive");
    }
   
  });
}

function buscarPc() {
  let Tabla = document.querySelector("#tabla");
  const CuerpoTabla = document.querySelector("#CuerpoTablaPc");
  let filas = document.querySelectorAll(".filapc");
  let ArrFilas = [...filas];
  let searchvalor = document.querySelector("#search").value;
  CuerpoTabla.remove();
  let CuerpoTabla2 = document.createElement("tbody");

  ArrFilas.map((fila) => {
    if (searchvalor == "activo") {
      ArrFilas.map((fila) => {
        if (fila.textContent.includes("activo")) {
          CuerpoTabla2.append(fila);
          Tabla.append(CuerpoTabla2);
          ArrFilas.map((fila) => {
            if (fila.textContent.includes("inactivo")) {
              fila.remove();
            }
          });
        }
      });
    } else {
      if (fila.textContent.includes(searchvalor)) {
        CuerpoTabla2.append(fila);
        Tabla.append(CuerpoTabla2);
      }
    }
  });
}

ocultandoFila();