
from flask import Flask, jsonify, request
from conexion import conn
from flask_login import login_required, LoginManager, login_user, logout_user
app = Flask(__name__)
from werkzeug.security import check_password_hash, generate_password_hash
#Models
from models.ModelUser import ModelUser

#Entities
from models.entities.User import User



login_manager_app = LoginManager(app)

@login_manager_app.user_loader
def load_user(user_id):
    return 


@app.route('/usuarios/login', methods=['POST'])
def login():
    if request.method == 'POST':
        request_data = request.get_json()
        cont = 0
        #Extraer datos del JSON
        email = request_data['Email']
        contra = request_data['Password']
        logged_user = False

        logged_user = verificarUsuario(email,contra)
        
        if logged_user == True:
            return "Bienvenido " + email
        elif logged_user == False:
            return "Usuario o contraseña incorrectos."
        else:
            return "Error inesperado."
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
            consulta = "INSERT INTO Usuarios (Username, Email, Password, Salt) VALUES ('" + usuario + "', '" + email + "','" + contra + "', '" + "1" + "')"
            cursor.execute(consulta)
        
        return "Usuario registrado correctamente."
    else:
        return "El usuario o el email ya existe."


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




def verificarUsuario(email, contra):
    try:
        with conn.cursor() as cursor:
            consulta = "SELECT * FROM Usuarios WHERE Email = '" + email + "'"
            cursor.execute(consulta)
            resultado = cursor.fetchone()
            print(resultado[3])
            if resultado == None:
                return False
            else:
                print("Llegue aca")
                if check_password_hash(resultado[3], contra):
                    return True
                else:
                    return False
    except Exception as e:
        print(e)
        return False

def verificarUnicoUsuario(email, username):
    try:
        with conn.cursor() as cursor:
            consulta = "SELECT * FROM Usuarios WHERE Email = '" + email + "' OR Username = '" + username + "'"
            cursor.execute(consulta)
            resultado = cursor.fetchone()
            if resultado == None:
                return True
            else:
                return False
    except Exception as e:
        return False



app.run(port=5000)