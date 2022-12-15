
from werkzeug.security import check_password_hash

class User():

    def __init__(self, username, password, email):
        self.username = username
        self.password = password
        self.email = email
        
    @classmethod
    def check_password(self, hash, password):
        print(check_password_hash(hash, password))
        return check_password_hash(hash, password)
    

