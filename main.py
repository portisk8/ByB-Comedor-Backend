from conexion import conn

try:
    print("Ingrese usuario y contraseña para registrarse:")
    usuario = input("Usuario: ")
    contraseña = input("Contraseña: ")

    print("Su usuario es: " + usuario)
    print("Su contraseña es: " + contraseña)

    with conn.cursor() as cursor:

        consulta = "INSERT INTO usuarios (usuario, contraseña) VALUES ('" + usuario + "', '" + contraseña + "')"
        cursor.execute(consulta)

    #Logeo
    while True:
        print("Ingrese usuario y contraseña para iniciar sesion:")
        usuarioL = input("Usuario: ")
        contraseñaL = input("Contraseña: ")

        with conn.cursor() as cursor:
            consulta = "SELECT * FROM usuarios WHERE usuario = '" + usuarioL + "' AND contraseña = '" + contraseñaL + "'"
            cursor.execute(consulta)
            resultado = cursor.fetchone()
            if resultado is None:
                print("Usuario o contraseña incorrectos")
            else:
                print("Bienvenido " + usuarioL)
                break    
except Exception as e:
    print(e)
    exit()
finally:
    conn.close()
    print("Conexion cerrada")
    exit()
