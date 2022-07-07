from flask import Flask, request, render_template, redirect
import os
import pyodbc

server = 'localhost'
database = 'ByB_Comedor'
usiario = 'asd'
password = 'asd'





try:
    conn = pyodbc.connect('DRIVER={ODBC Driver 17 for SQL Server};SERVER='+server+';DATABASE='+database+';UID='+usiario+';PWD='+ password)
    print('Conexion exitosa')
except:
    print('Conexion fallida')
    exit()