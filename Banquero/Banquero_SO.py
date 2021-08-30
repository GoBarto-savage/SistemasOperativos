class Proceso:
    def __init__(self, req, asig, nombre):
        self.nombre = nombre
        self.requerido = req
        self.asignado = asig
        self.termino = False


def check_not_done(Procesos):
    for proceso in Procesos:
        if(proceso.termino == False):
            return True
    return False


def printProc(Procesos, file):
    for proceso in Procesos:
        if(not proceso.termino):
            print(
                f"El proceso {proceso.nombre} tiene asignado{proceso.asignado} y requiere {proceso.requerido}\n")
            file.writelines(
                f"El proceso {proceso.nombre} tiene asignado{proceso.asignado} y requiere {proceso.requerido} \n")


# 4rec asignados + n disp
Procesos = [Proceso(3, 1, 'A'),
            Proceso(2, 1, 'B'),
            Proceso(1, 0, 'C'),
            Proceso(2, 1, 'D'),
            Proceso(3, 1, 'E'),
            ]

# CASO SIN BLOQUEO 3 REC EN ADELANTE CASO CON BLOQUEO USAR O 1 O 2 PROCESOS DISPONIBLES
recursos_disponibles = 3

bloqueo = False
with open("Resultados.txt", "w") as file:
    while(check_not_done(Procesos) and not bloqueo):

        printProc(Procesos, file)

        for proceso in Procesos:
            bloqueo = True
            print(f"recursos disponibles: {recursos_disponibles}")
            file.writelines(f"recursos disponibles: {recursos_disponibles} \n")

            if(proceso.requerido <= recursos_disponibles and not proceso.termino):
                bloqueo = False
                recursos_disponibles += proceso.asignado
                proceso.requerido = 0
                proceso.termino = True
                print(f"termino proceso {proceso.nombre}")
                file.writelines(f"termino proceso {proceso.nombre} \n")
            else:
                pass
    if(bloqueo):
        print("Ocurrio un bloqueo")
        file.writelines("Ocurrio un Bloqueo \n")
    else:
        print("Finalizo sin bloqueos")
        file.writelines("Finalizo sin bloqueos \n")
