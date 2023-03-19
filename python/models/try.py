
from conexion import conn

email = "Maquinola@riquelme.com"

try:
    with conn.cursor() as cursor:
        cursor = conn.cursor()
        cursor.execute("SELECT * FROM Usuarios WHERE Email = '" + email + "'")
        resultado = cursor.fetchone()
        if resultado is None:
            print( "Usuario inexistente.")
        else:
            print(resultado[1], resultado[2], resultado[3])
except Exception as e:
    print(e)
    print("Error en la consulta.")