let search = document.querySelector("#search");


search.addEventListener("input", () => {
    let filter = document.querySelectorAll(".filter");
    let ArrFilter = [...filter];
    let searchvalor = document.querySelector("#search").value;   

    ArrFilter.forEach(item => {
        const compInv = document.querySelectorAll(".inv").innerText;
        const Departamento = document.querySelector("#dep").innerText;
        const Area = document.querySelector("#area").innerText;
        const Nombre = document.querySelector("#nombre").innerText;
        const SistOper = document.querySelector("#so").innerText;
        const Estado = document.querySelector("#estado").innerText;
        const Mac = document.querySelector("#mac").innerText;
        const NumIp = document.querySelector("#NumIp").innerText;
        const IDusuario = document.querySelector("#IDusuario").innerText;
        const ImprInv = document.querySelector("#InvImpr").innerText;
        const InvTecl = document.querySelector("#InvTecl").innerText;
        const UserName = document.querySelector("#UserName").innerText;
        const BoardMarca = document.querySelector("#BoardMarca").innerText;
        const CapDisc = document.querySelector("#CapDisc").innerText;
        const TipoConexDisc = document.querySelector("#TipoConexDisc").innerText;
        const CapMem = document.querySelector("#CapMem").innerText;
        const TecMem = document.querySelector("#TecMem").innerText;
        const TecnMicro = document.querySelector("#TecnMicro").innerText;
        const TeclId = document.querySelector("#TeclId").innerText;
        const BoardId = document.querySelector("#BoardId").innerText;
        const ImprId = document.querySelector("#ImprId").innerText;

        if (searchvalor == item.innerText) {
             
            console.log(`Nombre: ${Nombre}\n
                        Inventario: ${compInv}\n
                    Departamento: ${Departamento}\n
                    Area: ${Area}\n
                    Sistema Operativo: ${SistOper}\n
                    Estado: ${Estado}\n
                    Mac: ${Mac}\n
                    Numero Ip: ${NumIp}\n   
                    Usuario No: ${IDusuario}\n
                    Inventario Impresora: ${ImprInv}\n
                    Inventario Teclado: ${InvTecl}\n
                    User name: ${UserName}\n
                    Marca board: ${BoardMarca}\n
                    Capacidad del disco: ${CapDisc}\n
                    Tipo De conexion de disco: ${TipoConexDisc}\n
                    Capacidad de memoria: ${CapMem}\n
                    Teclado No: ${TeclId}\n
                    Tecnologia del micro: ${TecnMicro}\n
                    Tecnologia de memoria: ${TecMem}\n
                    Board Id: ${BoardId}\n
                    Impresora Id: ${ImprId}\n
              `);
        }else
        {
            console.log("No encontamos lo que buscas")
        };
    });
});


