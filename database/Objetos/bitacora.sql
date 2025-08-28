-- Bitacora de cambios en la base de datos
CREATE TABLE IF NOT EXISTS bitacora (
    id SERIAL PRIMARY KEY,
    tabla VARCHAR(255) NOT NULL,
    operacion VARCHAR(50) NOT NULL,
    usuario VARCHAR(255) NOT NULL,
    fecha TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    detalles TEXT
);  

