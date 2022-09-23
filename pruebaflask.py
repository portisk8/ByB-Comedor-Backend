
from flask import Flask, jsonify, render_template, request
from config import config
from flask_cors import CORS, cross_origin

app = Flask(__name__, static_url_path='/static')
app.config['SECRET_KEY'] = 'the quick brown fox jumps over the lazy   dog'
#cors = CORS(app, resources={r"/foo": {"origins": "http://localhost:port"}})

@app.route('/', methods=['GET']) 
def index():
    return render_template('index.html')

@app.route('/articulos', methods=['POST'])
def articulos():
    data = request.get_json()
    return jsonify(data)

@app.route('/api', methods=['POST'])
def api():
    data = request.get_json()
    return jsonify(data)

@app.route('/api/user/login', methods=['POST'])
@cross_origin(origin='http://localhost:3000/',headers=['Content- Type','Authorization'])
def login():
    response = jsonify(request.get_json())
    response.headers.add('Access-Control-Allow-Origin', '*')
    return response



if __name__ == '__main__':
    app.config.from_object(config['development'])
    app.run()
