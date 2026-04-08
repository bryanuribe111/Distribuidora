CREATE DATABASE distribuidora_aseo;
USE distribuidora_aseo;

CREATE TABLE rol (
    id_rol INT  PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL UNIQUE,
    
    CHECK (nombre IN ('administrador', 'vendedor', 'fabricador'))
);

CREATE TABLE usuario (
    id_usuario INT  PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    correo VARCHAR(100) UNIQUE NOT NULL,
    contrasena VARCHAR(255) NOT NULL,
    id_rol INT NOT NULL,

    FOREIGN KEY (id_rol) REFERENCES rol(id_rol)
        ON UPDATE CASCADE
        ON DELETE NO ACTION
);

CREATE TABLE cliente (
    id_cliente INT  PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    direccion VARCHAR(150),
    tipo_cliente VARCHAR(50)
);

CREATE TABLE producto (
    id_producto INT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,

    tipo VARCHAR(20) NOT NULL
    CHECK (tipo IN ('transformado', 'no transformado')),

    precio_base DECIMAL(10,2) NOT NULL
    CHECK (precio_base >= 0),

    stock INT DEFAULT 0
    CHECK (stock >= 0),

    id_fabricador INT,

    FOREIGN KEY (id_fabricador) REFERENCES usuario(id_usuario)
    ON UPDATE CASCADE
    ON DELETE SET NULL
);

CREATE TABLE material (
    id_material INT  PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    stock INT DEFAULT 0 CHECK (stock >= 0),
    costo DECIMAL(10,2) NOT NULL CHECK (costo >= 0)
);

CREATE TABLE composicion_producto (
    id_producto INT,
    id_material INT,
    cantidad INT NOT NULL CHECK (cantidad > 0),

    PRIMARY KEY (id_producto, id_material),

    FOREIGN KEY (id_producto) REFERENCES producto(id_producto)
        ON DELETE CASCADE,
    FOREIGN KEY (id_material) REFERENCES material(id_material)
        ON DELETE CASCADE
);

CREATE TABLE pedido (
    id_pedido INT  PRIMARY KEY,
    fecha DATE NOT NULL,
    estado VARCHAR(50) DEFAULT 'pendiente',

    id_cliente INT NOT NULL,
    id_vendedor INT NOT NULL,

    FOREIGN KEY (id_cliente) REFERENCES cliente(id_cliente)
        ON UPDATE CASCADE
        ON DELETE NO ACTION,

    FOREIGN KEY (id_vendedor) REFERENCES usuario(id_usuario)
        ON UPDATE CASCADE
        ON DELETE NO ACTION
);

CREATE TABLE detalle_pedido (
    id_detalle INT  PRIMARY KEY,
    id_pedido INT NOT NULL,
    id_producto INT NOT NULL,
    cantidad INT NOT NULL CHECK (cantidad > 0),
    precio_unitario DECIMAL(10,2) NOT NULL CHECK (precio_unitario >= 0),

    FOREIGN KEY (id_pedido) REFERENCES pedido(id_pedido)
        ON DELETE CASCADE,

    FOREIGN KEY (id_producto) REFERENCES producto(id_producto)
        ON DELETE NO ACTION
);

CREATE TABLE compra (
    id_compra INT  PRIMARY KEY,
    proveedor VARCHAR(100) NOT NULL,
    fecha DATE NOT NULL,
    costo_total DECIMAL(10,2) CHECK (costo_total >= 0)
);

CREATE TABLE detalle_compra (
    id_detalle_compra INT  PRIMARY KEY,
    id_compra INT NOT NULL,
    id_producto INT NOT NULL,
    cantidad INT NOT NULL CHECK (cantidad > 0),
    costo_unitario DECIMAL(10,2) NOT NULL CHECK (costo_unitario >= 0),

    FOREIGN KEY (id_compra) REFERENCES compra(id_compra)
        ON DELETE CASCADE,

    FOREIGN KEY (id_producto) REFERENCES producto(id_producto)
        ON DELETE NO ACTION
);

CREATE TABLE precios_especiales (
    id_precio INT  PRIMARY KEY,
    id_cliente INT NOT NULL,
    id_producto INT NOT NULL,
    precio_especial DECIMAL(10,2) NOT NULL CHECK (precio_especial >= 0),

    UNIQUE (id_cliente, id_producto),

    FOREIGN KEY (id_cliente) REFERENCES cliente(id_cliente)
        ON DELETE CASCADE,

    FOREIGN KEY (id_producto) REFERENCES producto(id_producto)
        ON DELETE CASCADE
);

