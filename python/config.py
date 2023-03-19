from distutils.debug import DEBUG


class DevelopmentConfig():
    DEBUG = True
    HTML = True
    JS = True
    CSS = True

config = {
    'development': DevelopmentConfig
    
}