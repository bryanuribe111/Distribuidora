const API_PRODUCTOS = "http://localhost:5067/api/Productos";

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

    console.log(producto);

    try {

        const respuesta = await fetch(API_PRODUCTOS, {

            method: "POST",

            headers: {
                "Content-Type": "application/json"
            },

            body: JSON.stringify(producto)
        });

        const data = await respuesta.json();

        if (respuesta.ok) {

            alert(data.mensaje);

        } else {

            console.error(data);

            alert("Error al guardar producto");
        }

    } catch (error) {

        console.error(error);

        alert("Error de conexión con el servidor");
    }
}