const btnDark = document.querySelector(".theme-switcher");

mantenerestilo();

btnDark.addEventListener("click", ValidarsiEstaOscuro)


function ValidarsiEstaOscuro(){
    document.body.classList.toggle("darkmode");
    const tabladark = document.querySelector("table");
    const titulos = document.querySelector("h1, h2, h3, h4, .h1 , .h2 , .h3 , .h4") 
    if (document.body.classList.contains("darkmode")) {
        tabladark.classList.add("table-dark");
        titulos.classList.add("text-light");
    }
    else if(!document.body.classList.contains("darkmode")){
        tabladark.classList.Remove("table-dark");
        titulos.classList.Remove("text-light");
    }
}

function mantenerestilo(){
    const tabladark = document.querySelector("table");
    const titulos = document.querySelector("h1, h2, h3, h4, .h1 , .h2 , .h3 , .h4") 
    if (document.body.classList.contains("darkmode")) {
        tabladark.classList.add("table-dark");
        titulos.classList.add("text-light");
    }
    else{
        tabladark.classList.Remove("table-dark");
        titulos.classList.Remove("text-light");
    }
}