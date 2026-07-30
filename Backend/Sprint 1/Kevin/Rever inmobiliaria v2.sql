CREATE DATABASE rever_inmobiliaria_v2;
USE rever_inmobiliaria_v2;

-- =========================
-- ROLES
-- =========================
CREATE TABLE roles (
    id_rol INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

INSERT INTO roles (nombre) VALUES ('administrador'), ('vendedor'), ('comprador');

-- =========================
-- USUARIOS
-- =========================
CREATE TABLE usuarios (
    id_usuario INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    correo VARCHAR(100) UNIQUE NOT NULL,
    contraseña VARCHAR(255) NOT NULL, -- almacenar con bcrypt
    telefono VARCHAR(20) NOT NULL,
    id_rol INT NOT NULL,
    fecha_registro DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (id_rol) REFERENCES roles(id_rol)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- =========================
-- UBICACIONES
-- =========================
CREATE TABLE ciudades (
    id_ciudad INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE localidad (
    id_localidad INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE barrios (
    id_barrio INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    id_ciudad INT NOT NULL,
    id_localidad INT NOT NULL,
    FOREIGN KEY (id_ciudad) REFERENCES ciudades(id_ciudad),
    FOREIGN KEY (id_localidad) REFERENCES localidad(id_localidad)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- =========================
-- TIPOS DE INMUEBLE
-- =========================
CREATE TABLE tipos_inmueble (
    id_tipo INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

INSERT INTO tipos_inmueble (nombre) VALUES ('venta'), ('arriendo');

-- =========================
-- ESTADOS DE PUBLICACIÓN
-- =========================
CREATE TABLE estados_publicacion (
    id_estado INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

INSERT INTO estados_publicacion (nombre) VALUES ('pendiente'), ('aprobado'), ('rechazado');

-- =========================
-- INMUEBLES
-- =========================
CREATE TABLE inmuebles (
    id_inmueble INT AUTO_INCREMENT PRIMARY KEY,
    titulo VARCHAR(150) NOT NULL,
    descripcion TEXT NOT NULL,
    precio DECIMAL(12,2) NOT NULL,
    id_tipo INT NOT NULL,
    direccion VARCHAR(200) NOT NULL,
    id_barrio INT NOT NULL,
    habitaciones INT NOT NULL,
    baños INT NOT NULL,
    metros_cuadrados INT NOT NULL,
    estrato INT NOT NULL,
    latitud DECIMAL(10,8) NOT NULL,
    longitud DECIMAL(11,8) NOT NULL,
    id_usuario INT NOT NULL,
    id_estado INT NOT NULL DEFAULT 1,
    fecha_publicacion DATETIME DEFAULT CURRENT_TIMESTAMP,

    FOREIGN KEY (id_tipo) REFERENCES tipos_inmueble(id_tipo),
    FOREIGN KEY (id_barrio) REFERENCES barrios(id_barrio),
    FOREIGN KEY (id_usuario) REFERENCES usuarios(id_usuario),
    FOREIGN KEY (id_estado) REFERENCES estados_publicacion(id_estado)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- =========================
-- IMÁGENES
-- =========================
CREATE TABLE imagenes (
    id_imagen INT AUTO_INCREMENT PRIMARY KEY,
    url VARCHAR(255) NOT NULL,
    id_inmueble INT NOT NULL,
    FOREIGN KEY (id_inmueble) REFERENCES inmuebles(id_inmueble) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- =========================
-- CARACTERÍSTICAS
-- =========================
CREATE TABLE caracteristicas (
    id_caracteristica INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- RELACIÓN N:M
CREATE TABLE inmueble_caracteristica (
    id_inmueble INT NOT NULL,
    id_caracteristica INT NOT NULL,
    PRIMARY KEY (id_inmueble, id_caracteristica),
    FOREIGN KEY (id_inmueble) REFERENCES inmuebles(id_inmueble) ON DELETE CASCADE,
    FOREIGN KEY (id_caracteristica) REFERENCES caracteristicas(id_caracteristica) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- =========================
-- CONTACTOS
-- =========================
CREATE TABLE contactos (
    id_contacto INT AUTO_INCREMENT PRIMARY KEY,
    id_comprador INT NOT NULL,
    id_vendedor INT NOT NULL,
    id_inmueble INT NOT NULL,
    mensaje TEXT NOT NULL,
    fecha DATETIME DEFAULT CURRENT_TIMESTAMP,

    FOREIGN KEY (id_comprador) REFERENCES usuarios(id_usuario),
    FOREIGN KEY (id_vendedor) REFERENCES usuarios(id_usuario),
    FOREIGN KEY (id_inmueble) REFERENCES inmuebles(id_inmueble)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
