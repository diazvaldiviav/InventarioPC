const NombrePcInput = document.querySelector("#NombrePc");
const btnCardInactive = document.getElementById("btn-card-inactive");

btnCardInactive.addEventListener("click" , OcultarCard);


console.log("El script se agrego bien");
console.log(NombrePcInput);

NombrePcInput.addEventListener("input", ValidandoNombre);

function ValidandoNombre() {
    const NombrePc = document.querySelector("#NombrePc").value;
    //validacionNombre
    const textValidationNombreMax = document.querySelector("#validacionNombreMax"); 
    const textValidationNombreMin = document.querySelector("#validacionNombreMin"); 

    if (NombrePc.length > 15) {
        textValidationNombreMax.classList.remove("inactive");
        textValidationNombreMax.classList.add("text-danger");

    } else if (NombrePc.length < 13) {
        textValidationNombreMin.classList.remove("inactive");
        textValidationNombreMin.classList.add("text-danger");
        textValidationNombreMax.classList.add("inactive");
        
    } else if (NombrePc.length >= 13 && NombrePc.length < 15) {
        textValidationNombreMin.classList.add("inactive");
        textValidationNombreMax.classList.add("inactive");
        
    }

    console.log(NombrePc);

    
}

function OcultarCard() {
    const Card = document.getElementById("cardInactive");
    Card.classList.add("inactive");

    console.log("EL nuevo script funciona");
}


