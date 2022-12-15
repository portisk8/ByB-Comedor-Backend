
from flask import Flask, jsonify, request
from conexion import conn
from flask_login import login_required
app = Flask(__name__)
import bcrypt
import os
#Models
from models.ModelUser import ModelUser
auth = False
#Entities
from models.entities.User import User


@app.route('/usuarios/login', methods=['POST'])
def login():
    if request.method == 'POST':
        request_data = request.get_json()
        cont = 0
        #Extraer datos del JSON
        email = request_data['Email']
        contra = request_data['Password']
        print(email)
        user = User("asd",email, contra)
        print(user.email + " " + user.password)
        logged_user = ModelUser.login(conn, user)
        if logged_user != None:
            return "Bienvenido " + email
        else:
            return "Usuario o contraseña incorrectos."
        # with conn.cursor() as cursor:
        #     consulta = "SELECT * FROM Usuarios WHERE Email = '" + email + "' AND Password = '" + contraseña + "'"
        #     cursor.execute(consulta)
        #     resultado = cursor.fetchone()
        #     if resultado is None:
        #         return "Usuario o contraseña incorrectos."
        #     else:
        #         return ("Bienvenido " + email)
    else:
        return "Error en la consulta."

@app.route('/usuarios/reg', methods=['POST'])
def reg():
    request_data = request.get_json()
    
    #Extraer datos del JSON
    usuario = request_data['Username']
    contraseña = request_data['Password']
    email = request_data['Email']
    # Encriptar contraseña
    


    with conn.cursor() as cursor:
        consulta = "INSERT INTO Usuarios (Username, Email, Password, Salt) VALUES ('" + usuario + "', '" + email + "','" + contraseña + "', '" + "1" + "')"
        cursor.execute(consulta)
    
    return "Usuario registrado correctamente."


@app.route('/')
def home():
    return 'Hello World!'

@app.route('/comedores', methods=['GET'])
@login_required
def carga_comedores():
    with conn.cursor() as cursor:
        cursor.execute("SELECT * FROM Comedores")
        resultado = cursor.fetchall()
        return jsonify(resultado)

@app.route('/comedores/<int:id>', methods=['GET'])
@login_required
def carga_comedor(id):
    with conn.cursor() as cursor:
        cursor.execute("SELECT * FROM Comedores WHERE Id = " + str(id))
        resultado = cursor.fetchone()
        return jsonify(resultado)

@app.route('/comedores', methods=['POST'])
def crea_comedor():
    request_data = request.get_json()
    with conn.cursor() as cursor:
        cursor.execute("INSERT INTO Comedores (Descripcion, Titulo, DireccionCalle, DireccionNumero) VALUES ('" + request_data['Desc'] + "', '" + request_data['Titulo'] + "','" + request_data['DireccionCalle'] + "', '" + request_data['DireccionNumero'] + "')")
    return "Comedor creado correctamente."

@app.route('/eliminarDespues', methods=['GET'])
def eliminarDespues():
    email = "Maquinola@riquelme.com"
    with conn.cursor() as cursor:
        try:
            cursor.execute("SELECT * FROM Usuarios WHERE Email = '" + email + "'")
            resultado = cursor.fetchall()
            print(resultado[0][2])
        except:
            print("Error")
    return "Existe el usuario."

app.run(port=5000)