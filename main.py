import email
from conexion import conn

try:
    print("Ingrese usuario y contraseña para registrarse:")
    usuario = input("Usuario: ")
    contraseña = input("Contraseña: ")
    email = input("Email: ")
    intentos = 0
    print("Su usuario es: " + usuario)
    print("Su contraseña es: " + contraseña)

    with conn.cursor() as cursor:

        consulta = "INSERT INTO Usuarios (Username, Email, Password, Salt) VALUES ('" + usuario + "', '" + email + "','" + contraseña + "', '" + "1" + "')"
        cursor.execute(consulta)

    #Logeo
    while True:
        print("Ingrese usuario y contraseña para iniciar sesión:")
        usuarioL = input("Usuario: ")
        contraseñaL = input("Contraseña: ")

        with conn.cursor() as cursor:
            consulta = "SELECT * FROM Usuarios WHERE Username = '" + usuarioL + "' AND Password = '" + contraseñaL + "'"
            cursor.execute(consulta)
            resultado = cursor.fetchone()
            if resultado is None:
                intentos = intentos + 1
                if intentos == 3:
                    print("Ha excedido el número de intentos.")
                    break
                print("Usuario o contraseña incorrectos.")
            else:
                print("Bienvenido " + usuarioL)
                break    
except Exception as e:
    print("Error: " + str(e))
    exit()
finally:
    conn.close()
    print("Conexión cerrada.")
    exit()
