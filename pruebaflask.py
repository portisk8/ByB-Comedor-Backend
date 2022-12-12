
from flask import Flask, jsonify, request
from conexion import conn
app = Flask(__name__)


@app.route('/usuarios/login', methods=['POST'])
def login():
    request_data = request.get_json()

    #Extraer datos del JSON
    email = request_data['Email']
    contraseña = request_data['Password']

    with conn.cursor() as cursor:
        consulta = "SELECT * FROM Usuarios WHERE Email = '" + email + "' AND Password = '" + contraseña + "'"
        cursor.execute(consulta)
        resultado = cursor.fetchone()
        if resultado is None:
            return "Usuario o contraseña incorrectos."
        else:
            return ("Bienvenido " + email)


@app.route('/usuarios/reg', methods=['POST'])
def reg():
    request_data = request.get_json()
    
    #Extraer datos del JSON
    usuario = request_data['Username']
    contraseña = request_data['Password']
    email = request_data['Email']


    with conn.cursor() as cursor:
        consulta = "INSERT INTO Usuarios (Username, Email, Password, Salt) VALUES ('" + usuario + "', '" + email + "','" + contraseña + "', '" + "1" + "')"
        cursor.execute(consulta)
    
    return "Usuario registrado correctamente."


@app.route('/')
def home():
    return 'Hello World!'

app.run(port=5000)