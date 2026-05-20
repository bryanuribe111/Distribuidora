const API_PRODUCTOS = "http://localhost:5067/api/Productos";
const API_CLIENTES = "https://localhost:7067/api/Clientes";
const API_PEDIDOS = "https://localhost:7067/api/Pedidos";

let productoEditando = null;



function mostrarSeccion(id) {

    const secciones = document.querySelectorAll('.seccion');

    secciones.forEach(seccion => {
        seccion.classList.remove('activa');
    });

    document.getElementById(id).classList.add('activa');
}


async function guardarProducto() {

    const producto = {

        nombre: document.getElementById("nombreProducto").value,

        tipo: document.getElementById("tipoProducto").value,

        precioBase: parseFloat(
            document.getElementById("precioProducto").value
        ),

        stock: parseInt(
            document.getElementById("stockProducto").value
        ),

        idFabricador: parseInt(
            document.getElementById("fabricadorProducto").value
        )
    };



    if(producto.nombre === "") {

        alert("Ingrese el nombre del producto");

        return;
    }

    if(producto.tipo === "") {

        alert("Ingrese el tipo del producto");

        return;
    }

    if(isNaN(producto.precioBase) || producto.precioBase <= 0) {

        alert("Ingrese un precio válido");

        return;
    }

    if(isNaN(producto.stock) || producto.stock < 0) {

        alert("Ingrese un stock válido");

        return;
    }

    if(isNaN(producto.idFabricador) || producto.idFabricador <= 0) {

        alert("Ingrese un ID de fabricador válido");

        return;
    }

    try {

        let respuesta;


        if(productoEditando != null) {

            respuesta = await fetch(
                `${API_PRODUCTOS}/${productoEditando}`,
                {

                    method: "PUT",

                    headers: {
                        "Content-Type": "application/json"
                    },

                    body: JSON.stringify(producto)
                }
            );

        } else {



            respuesta = await fetch(API_PRODUCTOS, {

                method: "POST",

                headers: {
                    "Content-Type": "application/json"
                },

                body: JSON.stringify(producto)
            });
        }

        const data = await respuesta.json();

        if(respuesta.ok) {

            alert(data.mensaje);

            limpiarFormulario();

            listarProductos();

            productoEditando = null;

        } else {

            alert(data.mensaje);
        }

    } catch(error) {

        console.error(error);

        alert("Error de conexión con el servidor");
    }
}



async function listarProductos() {

    try {

        const respuesta = await fetch(API_PRODUCTOS);

        const productos = await respuesta.json();

        const tabla = document.getElementById("tablaProductos");

        tabla.innerHTML = "";

        productos.forEach(producto => {

            tabla.innerHTML += `

                <tr>

                    <td>${producto.idProducto}</td>

                    <td>${producto.nombre}</td>

                    <td>${producto.precioBase}</td>

                    <td>${producto.stock}</td>

                    <td>

                        <button onclick="editarProducto(${producto.idProducto})">
                            Editar
                        </button>

                        <button onclick="eliminarProducto(${producto.idProducto})">
                            Eliminar
                        </button>

                    </td>

                </tr>
            `;
        });

    } catch(error) {

        console.error(error);

        alert("Error al listar productos");
    }
}



async function eliminarProducto(id) {

    const confirmar = confirm(
        "¿Desea eliminar este producto?"
    );

    if(!confirmar) return;

    try {

        const respuesta = await fetch(
            `${API_PRODUCTOS}/${id}`,
            {
                method: "DELETE"
            }
        );

        const data = await respuesta.json();

        alert(data.mensaje);

        listarProductos();

    } catch(error) {

        console.error(error);

        alert("Error al eliminar producto");
    }
}



async function editarProducto(id) {

    try {

        const respuesta = await fetch(
            `${API_PRODUCTOS}/${id}`
        );

        const producto = await respuesta.json();

        document.getElementById("nombreProducto").value =
            producto.nombre;

        document.getElementById("tipoProducto").value =
            producto.tipo;

        document.getElementById("precioProducto").value =
            producto.precioBase;

        document.getElementById("stockProducto").value =
            producto.stock;

        document.getElementById("fabricadorProducto").value =
            producto.idFabricador;

        productoEditando = id;

    } catch(error) {

        console.error(error);

        alert("Error al cargar producto");
    }
}



function limpiarFormulario() {

    document.getElementById("nombreProducto").value = "";

    document.getElementById("tipoProducto").value = "";

    document.getElementById("precioProducto").value = "";

    document.getElementById("stockProducto").value = "";

    document.getElementById("fabricadorProducto").value = "";
}



window.onload = listarProductos;

async function guardarCliente() {

    const cliente = {

        nombre: document.getElementById("nombreCliente").value,

        telefono: document.getElementById("telefonoCliente").value,

        direccion: document.getElementById("direccionCliente").value
    };

    // VALIDACIONES

    if(cliente.nombre === "") {

        alert("Ingrese el nombre");

        return;
    }

    if(cliente.telefono === "") {

        alert("Ingrese el teléfono");

        return;
    }

    if(cliente.direccion === "") {

        alert("Ingrese la dirección");

        return;
    }

    try {

        const respuesta = await fetch(API_CLIENTES, {

            method: "POST",

            headers: {
                "Content-Type": "application/json"
            },

            body: JSON.stringify(cliente)
        });

        if(respuesta.ok) {

            alert("Cliente guardado correctamente");

            limpiarFormularioClientes();

            listarClientes();

        } else {

            alert("Error al guardar cliente");
        }

    } catch(error) {

        console.error(error);

        alert("Error de conexión");
    }
}

async function listarClientes() {

    try {

        const respuesta = await fetch(API_CLIENTES);

        const clientes = await respuesta.json();

        const tabla = document.getElementById("tablaClientes");

        tabla.innerHTML = "";

        clientes.forEach(cliente => {

            tabla.innerHTML += `

                <tr>

                    <td>${cliente.idCliente}</td>

                    <td>${cliente.nombre}</td>

                    <td>${cliente.telefono}</td>

                    <td>${cliente.direccion}</td>

                    <td>

                        <button onclick="eliminarCliente(${cliente.idCliente})">
                            Eliminar
                        </button>

                    </td>

                </tr>
            `;
        });

    } catch(error) {

        console.error(error);
    }
}

async function eliminarCliente(id) {

    const confirmar = confirm(
        "¿Desea eliminar este cliente?"
    );

    if(!confirmar) return;

    try {

        const respuesta = await fetch(
            `${API_CLIENTES}/${id}`,
            {
                method: "DELETE"
            }
        );

        if(respuesta.ok) {

            alert("Cliente eliminado");

            listarClientes();

        } else {

            alert("Error al eliminar");
        }

    } catch(error) {

        console.error(error);
    }
}

function limpiarFormularioClientes() {

    document.getElementById("nombreCliente").value = "";

    document.getElementById("telefonoCliente").value = "";

    document.getElementById("direccionCliente").value = "";
}

async function guardarPedido() {

 

    const idCliente = parseInt(
        document.getElementById("idClientePedido").value
    );

    const idProducto = parseInt(
        document.getElementById("idProductoPedido").value
    );

    const cantidad = parseInt(
        document.getElementById("cantidadPedido").value
    );

    const precio = parseFloat(
        document.getElementById("precioPedido").value
    );

    const idVendedor = parseInt(
        document.getElementById("idVendedorPedido").value
    );



    if(isNaN(idCliente) || idCliente <= 0) {

        alert("Ingrese un cliente válido");

        return;
    }

    if(isNaN(idProducto) || idProducto <= 0) {

        alert("Ingrese un producto válido");

        return;
    }

    if(isNaN(cantidad) || cantidad <= 0) {

        alert("Ingrese una cantidad válida");

        return;
    }

    if(isNaN(precio) || precio <= 0) {

        alert("Ingrese un precio válido");

        return;
    }

    if(isNaN(idVendedor) || idVendedor <= 0) {

        alert("Ingrese un vendedor válido");

        return;
    }


    const pedidoRequest = {

        pedido: {

            fecha: new Date().toISOString(),

            estado: "Pendiente",

            idCliente: idCliente,

            idVendedor: idVendedor
        },

        detalles: [

            {
                idProducto: idProducto,

                cantidad: cantidad,

                precioUnitario: precio
            }
        ]
    };

    console.log(pedidoRequest);



    try {

        const respuesta = await fetch(API_PEDIDOS, {

            method: "POST",

            headers: {
                "Content-Type": "application/json"
            },

            body: JSON.stringify(pedidoRequest)
        });

        const data = await respuesta.text();

        if(respuesta.ok) {

            alert(data);

            limpiarFormularioPedidos();

        } else {

            alert(data);
        }

    } catch(error) {

        console.error(error);

        alert("Error de conexión");
    }
}

function limpiarFormularioPedidos() {

    document.getElementById("idClientePedido").value = "";

    document.getElementById("idProductoPedido").value = "";

    document.getElementById("cantidadPedido").value = "";

    document.getElementById("precioPedido").value = "";

    document.getElementById("idVendedorPedido").value = "";
}