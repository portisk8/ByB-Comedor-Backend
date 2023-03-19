
from flask import Flask, jsonify, request
from conexion import conn
from flask_login import login_required, LoginManager, login_user, logout_user
app = Flask(__name__)
from werkzeug.security import check_password_hash, generate_password_hash
import os
#Models
from models.ModelUser import ModelUser

#Entities
from models.entities.User import User
app.secret_key = os.urandom(12)


login_manager_app = LoginManager(app)

@login_manager_app.user_loader
def load_user(id):
    return get_by_id(id)


@app.route('/usuarios/login', methods=['POST'])
def login():
    if request.method == 'POST':
        request_data = request.get_json()
        cont = 0
        #Extraer datos del JSON
        email = request_data['Email']
        contra = request_data['Password']

        logged_user = verificarUsuario(email,contra)
        
        if logged_user.password == True:
            login_user(logged_user)
            
            return "Bienvenido " + logged_user.username
        elif logged_user.password == False:
            return "Usuario o contraseña incorrectos."
        else:
            return "Usuario o contraseña incorrectos."
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
    if verificarUnicoUsuario(email, usuario):
        contra = generate_password_hash(contraseña)

        with conn.cursor() as cursor:
            cursor.execute("INSERT INTO Usuarios (Username, Email, Password, Salt) VALUES (?,?,?,?)",(usuario, email, contra, "1",))
        
        return "Usuario registrado correctamente."
    else:
        return "El usuario o el email ya existe."


@app.route('/')
def home():
    return 'Hello World!'

@app.route('/comedores', methods=['GET'])
@login_required
def carga_comedores():
    try:
        with conn.cursor() as cursor:
            cursor.execute("SELECT * FROM Comedores")
            resultado = cursor.fetchall()
            return jsonComedores(resultado)
    except Exception as e:
        print(e)
        return "Error en la consulta."

@app.route('/comedores/<int:id>', methods=['GET', 'PUT', 'DELETE'])
@login_required
def carga_comedor(id):
    #-----------------GET-----------------#
    if request.method == 'GET':
        try:
            with conn.cursor() as cursor:
                cursor.execute("SELECT * FROM Comedores WHERE ComedorId = ?" , (id,))
                resultado = cursor.fetchone()
                if resultado == None:
                    return "No existe el comedor."
                else:
                    comedorJson = {
                        "ComedorId": resultado[0],
                        "Descripcion": resultado[1],
                        "Titulo": resultado[2],
                        "DireccionCalle": resultado[3],
                        "DireccionNumero": resultado[4]
                    }
                    return jsonify(comedorJson)
        except Exception as e:
            print(e)
            return "Error en la consulta."
    
    #-----------------PUT-----------------#
    elif request.method == 'PUT':
        try:
            request_data = request.get_json()
            with conn.cursor() as cursor:
                cursor.execute("UPDATE Comedores SET Descripcion = ?, Titulo = ?, DireccionCalle = ?, DireccionNumero = ? WHERE ComedorId = ?", (request_data['Desc'], request_data['Titulo'], request_data['DireccionCalle'], request_data['DireccionNumero'],id,))
                
                
            return "Comedor actualizado correctamente."
        except Exception as e:
            print(e)
            return "Error en la consulta."
    
    #-----------------DELETE-----------------#
    elif request.method == 'DELETE':
        try:
            with conn.cursor() as cursor:
                cursor.execute("DELETE FROM Comedores WHERE ComedorId = ?", (id,))
            return "Comedor eliminado correctamente."
        except Exception as e:
            print(e)
            return "Error en la consulta."

            


@app.route('/comedores', methods=['POST'])
@login_required
def crea_comedor():
    request_data = request.get_json()
    with conn.cursor() as cursor:
        cursor.execute("INSERT INTO Comedores (Descripcion, Titulo, DireccionCalle, DireccionNumero) VALUES (?,?,?,?)", (request_data['Desc'], request_data['Titulo'], request_data['DireccionCalle'], request_data['DireccionNumero'],))
    return "Comedor creado correctamente."




def verificarUsuario(email, contra):
    try:
        with conn.cursor() as cursor:
            cursor.execute("SELECT * FROM Usuarios WHERE Email = ?", (email,))
            resultado = cursor.fetchone()
            print(resultado)
            if resultado == None:
                logged_user = User("", "", False)
                return logged_user
            else:
                print("Llegue aca")
                if check_password_hash(resultado[3], contra):
                    logged_user = User(resultado[0],resultado[2], resultado[1], True)
                    return logged_user
                else:
                    logged_user = User("", "", False)
                    return logged_user
    except Exception as e:
        logged_user = User("", "", False)
        return logged_user

def verificarUnicoUsuario(email, username):
    try:
        with conn.cursor() as cursor:
            cursor.execute("SELECT * FROM Usuarios WHERE Email = ? OR Username = ?", (email, username,))
            resultado = cursor.fetchone()
            if resultado == None:
                return True
            else:
                return False
    except Exception as e:
        return False


def get_by_id(id):
    try:
        with conn.cursor() as cursor:
            
            cursor.execute("SELECT * FROM Usuarios WHERE UsuarioId = ?", (id,))
            resultado = cursor.fetchone()
            if resultado == None:
                return None
            else:
                user = User(resultado[2], resultado[1], True)
                return user
    except Exception as e:
        return None



def jsonComedores(comedores):
    lista = []
    for comedor in comedores:
        lista.append({
            "Id": comedor[0],
            "Descripcion": comedor[1],
            "Titulo": comedor[2],
            "DireccionCalle": comedor[3],
            "DireccionNumero": comedor[4]
        })
    return jsonify(lista)

    
    


app.run(port=5000)