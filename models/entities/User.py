
from werkzeug.security import check_password_hash
from flask_login import UserMixin

class User(UserMixin):

    def __init__(self, email, username = None, password= "") -> None:
        self.username = username
        self.password = password
        self.email = email
        
    @classmethod
    def check_password(self, hash, password):
        print(check_password_hash(hash, password))
        return check_password_hash(hash, password)
    

