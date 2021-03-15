# UnFair

# Introducción

## Descripción breve del concepto

Juego de minijuegos con historia que se descubre a base de pistas conforme superas los minijuegos. Un*fair está ambientado en una feria norteamericana de los años 50 y los protagonistas son dos niños que quieren llegar a montarse en la noria.

## Descripción breve de la historia y personajes

La historia se desarrolla alrededor de dos niños pasando una tarde en la feria de su pequeña ciudad, una niña y un niño, siendo la niña el único personaje jugable y el niño su mejor amigo. Su objetivo es montar en la noria pero para ello necesitan una gran cantidad de tickets de muchos tipos distintos. 
Mientras avanza la historia se irán proporcionando pistas sobre la situación en la que se encuentran realmente.
Finalmente llegarán a la noria y empiezan a hablar de los días que pasaban juntos en la feria y cómo los echan de menos, en concreto del último día que pasaron juntos, de cómo no pudieron montar en la noria y del trágico accidente que sucedió en el camino a casa. Cuando acaba el viaje la protagonista baja sola de la atracción descubriéndose que el amigo en realidad murió. 

## Propósito, público objetivo y plataformas

El propósito de Un*fair es contar una historia y, al mismo tiempo, proporcionar una experiencia divertida.
Un*fair está dirigido a un público de todas las edades.
El juego estará disponible para PC.

# Planificación

Con el objetivo de planificar el trabajo y sacar el máximo partido a nuestro esfuerzo se ha decidido usar las dinámicas de la metodología ágil Scrum que más se adaptan al proyecto. Entre ellas destacan la realización de Daily StandUps en las que cada miembro del equipo informa al resto del estado de su trabajo y las Sprint Reviews al final de cada semana que servirán para poner en común lo que se ha avanzado y cómo encauzar el resto del trabajo.

También se ha decidido emplear tres horas diarias para trabajar en este proyecto. Esta cantidad de tiempo puede variar conforme avance el desarrollo.

## Hoja de ruta del desarrollo
* Primer prototipo del escenario general: 14 de marzo
* Versión alfa: 16 de marzo
* Versión beta de los minijuegos cerrada: 1 de abril
* Estructuración de la narrativa:  1 de abril
* Integración de minijuegos y escenario general: 8 de abril
* Versión beta del juego completo: 16 de abril
* Post-processing integrado: 11 de mayo
* Sonido integrado: 11 de mayo
* Interfaz final integrado: 11 de mayo
* Versión final de Un*fair: 20 de mayo

# Mecánicas de Juego y Elementos de Juego

## Descripción detallada del concepto de juego:
En el juego existirá un escenario completo por el que el jugador podrá moverse para empezar a jugar al minijuego deseado. Estos minijuegos se encontrarán en diferentes zonas de la feria y otorgarán tickets al jugador que le permitirán rejugar a los mismos o finalmente acceder a la feria.
Así mismo, el jugador podrá entablar conversación con los distintos personajes repartidos por el escenario, que le desvelarán poco a poco la historia. 

## Lista de Minijuegos:
* Tiro al blanco:
* Pesca de patos
* Caballos de carreras
* Encestar aros

# Descripción detallada de las mecánicas de juego:

## Tiro al blanco:

### 1. Concepto
Dianas aparecen en pantalla según son destruidas dejando siempre en pantalla 3 dianas a las que disparar. Cuando se acierta el jugador obtiene puntos, en cambio si falla se le resta una cantidad de puntos. Cada cierto tiempo una de esas dianas aparece dorada la cual da mas puntos y dura x tiempo en pantalla. Todo esto ocurre mientras hay una cuenta atrás.
 
### 2. Cómo ganar
El juego se completa al alcanzar un número de puntos. Los puntos aumentan conforme se acierta a dar dianas. Según la puntuación obtenida se conseguirán 1, 2 o 3 estrellas. 

### 3. IA
Movimiento de las dianas y que sean más torpes si eres malillo.

### 4. Ejemplos
* https://www.youtube.com/watch?v=RsqLO3gWJp0 [Variedad de dianas]
* https://www.youtube.com/watch?v=_R1yR1hbcNM [Tipos de tiro al blanco]
* https://www.youtube.com/watch?v=_R1yR1hbcNM [Reloj en el min 7:13]

## Pesca de patos:

### 1. Concepto
En una charca hay patos. El juego es “multijugador”, es decir se juega contra otro NPC.
### 2. Cómo ganar
Se gana consiguiendo más patos que el otro jugador. Según la puntuación obtenida se conseguirán 1, 2 o 3 estrellas.
### 3. IA
Movimiento de los patos, IA del NPC.

## Carrera de caballos:

### 1. Concepto
En una carrera de caballos se elige el caballo ganador . Debes realizar un combo de teclas para poder avanzar, si lo fallas, el caballo se atasca y necesitas realizar otro diferente para avanzar. Hay un máximo de 3 estrellas, si te atascas una vez al menos, el máximo pasa a ser 2. Para conseguir la segunda estrella deberá ganarse la carrera en un tiempo determinado.

### 2. Cómo ganar
Llegando primero a la meta.

### 3. IA
IA de los caballos, se mueven más despacio o rápido en función de la habilidad del jugador. Cuanto mejor lo haga, los caballos irán más deprisa. Si por el contrario, el jugador lo hace peor, los caballo irán más despacio o se atascarán más para darle ventaja al jugador.

# Trasfondo 

## Descripción detallada de la historia y la trama

### Personajes

* __Niña__
* __Niño__
* __Pareja de yayos__
  * X e Y llevan el puesto de XXXX. Son una pareja de ancianos que lleva muchos años en el negocio. Siempre están discutiendo pero se aprecian (classic)
* __Señor raro__
  * Sus comentarios no tienen mucho sentido en la mayoría de las ocasiones pero las pistas que ofrece son muy reveladoras.
* __El pequeño mequetrefe piltrafilla__
  * No lleva ningún puesto. Se dedica a XXX. Es un pillo.
* __Dos caras__
  * Dos de los puestos de la feria están dirigidos por X e Y. Estos personajes solo se diferencian físicamente en XXX. No se deja claro si son dos personas diferentes o solo uno de ellos cambiándose de ropa. En ningún momento se les ve en el mismo sitio. Sus personalidades son completamente opuestas
* __La acróbata__


### Entornos y lugares
* __La feria (escenario general)__
  * La feria es el escenario principal. En ella están situados todos los puestos. No se puede salir del recinto.
* __Puesto Tiro al plato__
* __Puesto Pesca de patos__
* __Puesto Carrera de caballos__
* __Puesto de la pitonisa__
  * La pitonisa es un autómata que a cambio de tickets te ofrece “predicciones a cambio de tickets”. En un principio estará cerrada, los niños deberán ir a la sala de los espejos a buscar X (al mecánico o alguna pieza o su pañuelo o cualquier cosa que pida o que se necesite).
* __La noria__
  * Último escenario del juego. En ella se revela la verdad sobre la historia.
* __Puesto de “comida”__
* __Casa de terror__
* __Tiovivo__
* __Sala de los espejos__
