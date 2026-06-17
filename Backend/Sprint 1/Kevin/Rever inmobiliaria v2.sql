CREATE DATABASE rever_inmobiliaria_v2;
USE rever_inmobiliaria_v2;

-- =========================
-- ROLES
-- =========================
CREATE TABLE roles (
    id_rol INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL
);

INSERT INTO roles (nombre) VALUES 
('administrador'),
('vendedor'),
('comprador');

-- =========================
-- USUARIOS
-- =========================
CREATE TABLE usuarios (
    id_usuario INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100),
    correo VARCHAR(100) UNIQUE NOT NULL,
    contraseña VARCHAR(255) NOT NULL, -- almacenar con bcrypt
    telefono VARCHAR(20),
    id_rol INT,
    fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (id_rol) REFERENCES roles(id_rol)
);

-- =========================
-- UBICACIÓN NORMALIZADA
-- =========================
CREATE TABLE ciudades (
    id_ciudad INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100)
);

CREATE TABLE barrios (
    id_barrio INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100),
    id_ciudad INT,
    FOREIGN KEY (id_ciudad) REFERENCES ciudades(id_ciudad)
);

-- =========================
-- TIPOS DE INMUEBLE
-- =========================
CREATE TABLE tipos_inmueble (
    id_tipo INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50)
);

INSERT INTO tipos_inmueble (nombre) VALUES
('venta'),
('arriendo');

-- =========================
-- ESTADOS
-- =========================
CREATE TABLE estados_publicacion (
    id_estado INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50)
);

INSERT INTO estados_publicacion (nombre) VALUES
('pendiente'),
('aprobado'),
('rechazado');

-- =========================
-- INMUEBLES
-- =========================
CREATE TABLE inmuebles (
    id_inmueble INT AUTO_INCREMENT PRIMARY KEY,
    titulo VARCHAR(150),
    descripcion TEXT,
    precio DECIMAL(12,2),
    id_tipo INT,
    direccion VARCHAR(200),
    id_barrio INT,
    habitaciones INT,
    baños INT,
    metros_cuadrados INT,
    estrato INT,
    latitud DECIMAL(10,8),
    longitud DECIMAL(11,8),
    id_usuario INT,
    id_estado INT DEFAULT 1,
    fecha_publicacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    FOREIGN KEY (id_tipo) REFERENCES tipos_inmueble(id_tipo),
    FOREIGN KEY (id_barrio) REFERENCES barrios(id_barrio),
    FOREIGN KEY (id_usuario) REFERENCES usuarios(id_usuario),
    FOREIGN KEY (id_estado) REFERENCES estados_publicacion(id_estado)
);

-- =========================
-- IMÁGENES
-- =========================
CREATE TABLE imagenes (
    id_imagen INT AUTO_INCREMENT PRIMARY KEY,
    url VARCHAR(255),
    id_inmueble INT,
    FOREIGN KEY (id_inmueble) REFERENCES inmuebles(id_inmueble)
);

-- =========================
-- CARACTERÍSTICAS
-- =========================
CREATE TABLE caracteristicas (
    id_caracteristica INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100)
);

-- RELACIÓN N:M
CREATE TABLE inmueble_caracteristica (
    id_inmueble INT,
    id_caracteristica INT,
    PRIMARY KEY (id_inmueble, id_caracteristica),
    FOREIGN KEY (id_inmueble) REFERENCES inmuebles(id_inmueble),
    FOREIGN KEY (id_caracteristica) REFERENCES caracteristicas(id_caracteristica)
);

-- =========================
-- CONTACTOS
-- =========================
CREATE TABLE contactos (
    id_contacto INT AUTO_INCREMENT PRIMARY KEY,
    id_comprador INT,
    id_vendedor INT,
    id_inmueble INT,
    mensaje TEXT,
    fecha TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    FOREIGN KEY (id_comprador) REFERENCES usuarios(id_usuario),
    FOREIGN KEY (id_vendedor) REFERENCES usuarios(id_usuario),
    FOREIGN KEY (id_inmueble) REFERENCES inmuebles(id_inmueble)
);