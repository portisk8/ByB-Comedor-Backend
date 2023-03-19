
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

        user = User(email, "", contra)
        user = ModelUser.login(conn, user)

        if user != None:
            logged_user = True
            login_user(user)
            return "Bienvenido " + email
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
    
    contra = generate_password_hash(contraseña)

    with conn.cursor() as cursor:
        consulta = "INSERT INTO Usuarios (Username, Email, Password, Salt) VALUES ('" + usuario + "', '" + email + "','" + contra + "', '" + "1" + "')"
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




def verificarUsuario(email, contra):
    try:
        with conn.cursor() as cursor:
            consulta = "SELECT * FROM Usuarios WHERE Email = '" + email + "'"
            cursor.execute(consulta)
            resultado = cursor.fetchone()
            
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


def get_by_id(id):
    try:
        pass
    except:
        pass


app.run(port=5000)