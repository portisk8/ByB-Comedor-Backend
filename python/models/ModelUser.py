from .entities.User import User


class ModelUser():
    @classmethod
    def login(self, db, user):
        try:
            print("LLegue aca")
            with db.cursor() as cursor:
                consulta = "SELECT * FROM Usuarios WHERE Email = " + user.email
                cursor.execute(consulta)

                resultado = cursor.fetchone()
                print("Resultado:" + str(resultado))
                if resultado == None:
                    return None
                else:
                    print(resultado[0][1], resultado[0][2])
                    user = User(resultado[0][1], resultado[0][2], User.check_password(resultado[0][3], user.password))
        except Exception as e:
            print(e)
            return "Error en la consulta."
    
    @classmethod
    def get_by_id(self, db, id):
        try:
            print("LLegue aca")
            with db.cursor() as cursor:
                consulta = "SELECT * FROM Usuarios WHERE UsuarioId = " + id
                cursor.execute(consulta)

                resultado = cursor.fetchone()
                print("Resultado:" + str(resultado))
                if resultado == None:
                    return None
                else:
                    print(resultado[0][1], resultado[0][2])
                    user = User(resultado[0][1], resultado[0][2], User.check_password(resultado[0][3], user.password))
        except Exception as e:
            print(e)
            return "Error en la consulta."


