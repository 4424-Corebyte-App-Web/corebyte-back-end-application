workspace "Corebyte" "TraceWine"{

    model {
        consumidor = person "PERSONA" "Compra el producto distribuido"
        productor = person "PRODUCTOR" "Gestiona el proceso de producción de vino"
        distribuidor = person "DISTRIBUIDOR" "Realizar y seguir los pedidos"
        pago = softwareSystem "SISTEMA DE PAGO" "Te permite realizar pagos dentro de la plataforma."
        Corebyte = softwareSystem "TRACEWINE SOFTWARE SYSTEM" "Solución integral para la gestión del proceso productivo del vino y pisco"{
            landing = container "LANDING PAGE" "Primera interacción con la plataforma: explicación de sus beneficios"
            single = container " SINGLE PAGE APPLICATION" "Aplicación para gestionar inventarios y funciones de pedidos"
            database = container "DATABASE" "Almacenamiento y entrega de datos a los usuarios"
            api = container "API APLICATION" "Lógica de backend que conecta el spa con la base de datos"{
                c1 = component "Controlador de inventario" ""
                c2 = component "Servicio de gestión de inventario" ""
                c3 = component "Servicio de consulta de inventario" ""
                c4 = component "Repositorio de inventario" ""
                c5 = component "Controlador de vinificación" ""
                c6 = component "Controlador de clientes" ""
                c7 = component "Servicio de consulta de vinificación" ""
                c8 = component "Servicios de gestión de vinificación" ""
                c9 = component "Servicio de gestión de clientes" ""
                c10 = component "Servicio de consulta de clientes" ""
                c11 = component "Repositorio de vinificación" ""
                c12 = component "Repositorio de clientes" ""
                c13 = component "Controlador de pedidos" ""
                c14 = component "Servicio de gestión de pedidos" ""
                c15 = component "Servicio de consulta de pedidos" ""
                c16 = component "Repositorio de pedidos" ""
            }
        }
        
        //contexto
    consumidor -> Corebyte "usa"
    productor -> Corebyte "Administra tus credenciales y perfil"
    distribuidor -> Corebyte "Realiza y sigue ventas"
    Corebyte -> pago "Obtiene información de usuario para registrarte"
    
    //container
    consumidor -> landing "usa"
    productor -> landing "Gestiona tus credenciales y perfil" 
    distribuidor -> landing "Realiza y sigue ventas"
    productor -> single "Gestiona tus servicios y ventas" 
    distribuidor -> single "Compra vinos y continúa tu viaje"
    landing -> single "Envía al usuario a la aplicación principal"
    single -> api "Realizar llamadas a la API"
    api -> database "Leer y escribir datos"
    api -> pago "Obtiene información del usuario para registrarse"
    
    //component
    single -> c1 "Realiza llamadas API"
    single -> c5 "Realiza llamadas API"
    single -> c6 "Realiza llamadas API"
    single -> c13 "Realiza llamadas API"
    c1 -> c2 "usa"
    c1 -> c3 "usa"
    c2 -> c4 "usa"
    c3 -> c4 "usa"
    c4 -> database "Lee y escribe"
    c5 -> c7 "usa"
    c5 -> c8 "usa"
    c6 -> c9 "usa"
    c6 -> c10 "usa"
    c7 -> c11 "usa"
    c8 -> c11 "usa"
    c9 -> c12 "usa"
    c10 -> c12 "usa"
    c11 -> database "Lee y escribe"
    c12 -> database "Lee y escribe"
    c13 -> c14 "usa"
    c13 -> c15 "usa"
    c14 -> c16 "usa"
    c15 -> c16 "usa"
    c16 -> database "Lee y escribe"
    }
    


    views {
    systemContext Corebyte {
        include *
        //autolayout
        
    }
    container Corebyte {
        include *
        //autolayout
    }
    component api {
        include *
        //autolayout
    }
    theme default
        
    }
}
