# UnFair

# Introducción

## Descripción breve del concepto

Juego de minijuegos con historia que se descubre a base de pistas conforme superas los minijuegos. Un\*fair está ambientado en una feria norteamericana de los años 50 y los protagonistas son dos niños cuyo objetivo es montarse en la noria.

## Descripción breve de la historia y personajes

La historia se desarrolla alrededor de dos niños pasando una tarde en la feria de su pequeña ciudad, una niña y un niño, siendo la niña el único personaje jugable y el niño su mejor amigo. Su objetivo es montar en la noria pero para ello necesitan una gran cantidad de tickets. 
Mientras avanza la historia se irán proporcionando pistas sobre la situación en la que se encuentran realmente.

Finalmente llegarán a la noria y empiezan a hablar de los días que pasaban juntos en la feria y cómo los echan de menos, en concreto del último día que pasaron juntos, de cómo no pudieron montar en la noria y del trágico accidente que sucedió. Cuando acaba el viaje la protagonista baja sola de la atracción descubriéndose que el amigo en realidad murió. 

## Propósito, público objetivo y plataformas

El propósito de Un\*fair es contar una historia y, al mismo tiempo, proporcionar una experiencia divertida.
Un\*fair está dirigido a un público de todas las edades.
El juego estará disponible para PC.

# Mecánicas de Juego y Elementos de Juego

## Descripción detallada del concepto de juego:
En el juego existirá un escenario completo por el que el jugador podrá moverse para empezar a jugar al minijuego deseado. Estos minijuegos se encontrarán en diferentes zonas de la feria y otorgarán tickets al jugador que le permitirán finalmente acceder a la noria.
Así mismo, el jugador podrá entablar conversación con los distintos personajes repartidos por el escenario, que le desvelarán poco a poco la historia. 

## Lista de Minijuegos:
* Tiro al blanco
* Pesca de patos
* Caballos de carreras
* Lanzamiento de canicas

## Sistema de Dificultad Dinámica (DDS)
Mientras se juega a un minijuego, las acciones del jugador se registran, con el fin de obtener un valor numérico que represente el nivel de habilidad del jugador, entre 0 y 1.

Cada una de las acciones registradas es un **input**, y tiene un **peso**, que indica la importancia que tiene en el cálculo total. La suma de todos los pesos debe ser exactamente 1.

A su vez, la IA de cada minijuego tiene acceso a una serie de **outputs**, que devolverán un valor distinto según el nivel de habilidad del jugador.

# Descripción detallada de las mecánicas de juego:

## Tiro al blanco:

### 1. Concepto
Dianas aparecen en pantalla según son destruidas dejando siempre en pantalla 3 dianas a las que disparar. Cuando se acierta el jugador obtiene puntos (10), en cambio si falla se le resta una cantidad de puntos (5). Cada cierto tiempo una de esas dianas aparece dorada la cual da mas puntos (50) y dura 2 segundos en pantalla. Todo esto ocurre mientras hay una cuenta atrás. Esta cuenta atrás puede crecer disparando a dianas de reloj que suman 5 segundos al tiempo que tiene el jugador (Estas dianas funcionan igual que las doradas solo que suman tiempo y son menos probables de aparecer).
 
### 2. Victoria y obtención de estrellas
El juego se completa al obtener una o más estrellas. Los puntos aumentan conforme se dispara a las dianas. Según la puntuación obtenida se conseguirán 1, 2 o 3 estrellas. Los puntos para conseguir las estrellas son 100, 250 y  500 puntos respectivamente.

### 3. IA
Ratio de dianas doradas y de tiempo dependiendo de la dificultad. Las dianas en movimiento se pueden implementar más adelante.

### 4. Ejemplos
* https://www.youtube.com/watch?v=RsqLO3gWJp0 [Variedad de dianas]
* https://www.youtube.com/watch?v=_R1yR1hbcNM [Tipos de tiro al blanco]
* https://www.youtube.com/watch?v=_R1yR1hbcNM [Reloj en el min 7:13]

## Pesca de patos:

### 1. Concepto
Hay una piscina con agua. Cada 3 segundos, aparecen 4 patos. El juego es “multijugador”, pues se juega contra otro NPC.
Hay cinco tipos de patos:

1. Pato normal: Otorga un punto a quien lo pesque
2. Pato verde: Otorga dos puntos al pescador controlado por el jugador
3. Pato azul: Añade 5 segundos al tiempo restante de la partida
4. Pato dorado: Otorga cinco puntos a quien lo pesque
5. Pato morado: Quita dos puntos a quien lo pesque
6. Pato supremo: Aparece al final de la partida, otorga siete puntos a quien lo pesque

Además, el jugador podrá depositar los patos en dos cestas diferentes:

* La primera cesta le otorgará los puntos correspondientes según el pato pescado
* La segunda cesta no otorgará puntos, pero le proporcionará un _boost_ de velocidad cuya duración equivale a la cantidad de puntos que otorgaría el pato pescado

### 2. Victoria y obtención de estrellas
Se gana consiguiendo más patos que el otro jugador. Los requisitos para obtener cada una de las estrellas son los siguientes:

* Conseguir más puntos que el otro jugador (requisito mínimo para obtener más estrellas)
* Conseguir por lo menos cinco puntos más que el otro jugador
* Conseguir no pescar patos morados (puede que cambie en el futuro)

### 3. IA
La IA controla el movimiento de la caña enemiga, e intenta buscar el mejor pato para pescar.

Para conseguirlo, comprueba los patos más cercanos y escoge el mejor.

Los inputs del DDS utilizados en este minijuego son:

1. Cuanto de bueno es el pato que acaba de pescar

2. La cantidad de estrellas obtenidas por el jugador

3. La diferencia de puntuación entre el jugador y la IA

4. Si el pato que se acaba de pescar es bueno o malo para el jugador

Los outputs del DDS utilizados en este minijuego son:

1. El radio de búsqueda de la IA

2. La tolerancia a pescar patos peores

3. El tiempo que espera la IA tras pescar un pato

## Carrera de caballos:

### 1. Concepto
Deben realizarse combos de teclas para poder avanzar, si lo fallas, el caballo no avanzará mientras que los rivales seguirán avanzando.

### 2. Victoria y obtención de estrellas
Para obtener la primera estrella el jugador deberá llegar el primero a la meta. Si el jugador va en busca de la segunda estrella deberá quedar en primera posición y acabar la carrera en 30 segundos o menos. Por último, para conseguir las tres estrellas, deben realizarse los mismos hitos anteriores además de no fallar ningún combo. Si no se consigue ni una estrella, el jugador habrá perdido la carrera.

### 3. IA
IA de los caballos rivales, se mueven más despacio o rápido en función de la habilidad del jugador. Cuanto mejor lo haga, los caballos irán más deprisa. Si por el contrario, el jugador lo hace peor, los caballo irán más despacio o se atascarán más para darle ventaja al jugador.

 ## Lanzamiento de canicas

### 1. Concepto
El jugador lanzará canicas en una rampa inclinada con diferentes agujeros. A la hora de lanzar, el jugador verá una trayectoria para saber hacia dónde se dirigirá la pelota al lanzarla. El jugador podrá girar para elegir la trayectoria y deberá esquivar obstáculos para llegar a los agujeros con mayor puntuación. Dependiendo del agujero, este otorgará al jugador mayor o menor puntuación.

### 2. Victoria y obtención de estrellas
El jugador solo perderá en caso de no introducir ninguna pelota en los agujeros. Así, conseguirá la primera estrella. Para la segunda estrella deberá alcanzar una puntuación determinada (50 puntos) y para la tercera estrella, alcanzar la puntuación y encestar todas las canicas.

### 3. IA
La IA de este minijuego será la encargada de mover y hacer aparecer los obstáculos para incluir cierta dificultad extra al minijuego. En caso de que el jugador sea experto, aparecerán más y se moverán más rápido mientras que si se le da mal, aparecerán menos obstáculos y su movimiento será menor.

# Trasfondo 

## Descripción detallada de la historia y la trama

### Personajes

* __Niña__
* __Niño__
* __Pareja de ancianos__
  * X e Y llevan el puesto de XXXX. Son una pareja de ancianos que lleva muchos años en el negocio. Siempre están discutiendo pero se aprecian (classic)
* __Señor raro__
  * Sus comentarios no tienen mucho sentido en la mayoría de las ocasiones pero las pistas que ofrece son muy reveladoras.
* __El pillo__
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
* __Puesto Lanzamiento de canicas__
* __Puesto de la pitonisa__
  * La pitonisa es un autómata que a cambio de tickets te ofrece “predicciones a cambio de tickets”. En un principio estará cerrada, los niños deberán ir a la sala de los espejos a buscar X (al mecánico o alguna pieza o su pañuelo o cualquier cosa que pida o que se necesite).
* __La noria__
  * Último escenario del juego. En ella se revela la verdad sobre la historia.
* __Puesto de “comida”__
* __Casa de terror__
* __Tiovivo__
* __Sala de los espejos__

# Arte
## Estética general del juego
La estética de Un\*fair plantea escenarios en 3D y personajes en 2D. La vista es en tercera persona mientras el personaje se encuentra en el escenario general. Cuando un jugador entra en algún puesto la cámara se adapta a la mejor vista para el minijuego planteado.
## Apartado visual
En general se pretende reflejar un estilo retro donde se impongan los colores ocre. El escenario se inspira en las ferias ambulantes estadounidenses típicas de los años cincuenta. Destaca el uso de la madera para los puestos con un acabado tosco que deje claro que son puestos desmontables de no muy buena calidad. Muchos de estos puestos contarán con una mano de pintura en forma de decoración ligeramente desgastada.
En cuanto a los personajes, siguen una estética cartoon, su ropa y estilo de peinado se inspiran en lo característico de la época entre las clases humildes.
## Referencias visuales
A la hora de realizar los modelos 3D se han tomado como referencia fotografías de ferias estadounidenses de los años 50. Las fotografías muestran ferias no ambulantes por lo que hemos adaptado los modelos a un diseño más simple, buscando de esta manera la coherencia con la historia y la ambientación.
## Arte 2D
### Concept Art
Comparativa de tamaños de los elementos del puesto:
![](https://github.com/DEVastationStudio/UnFair/blob/main/ReadmeImages/Comparativa.png)

Concepto del puesto de la mesa tambaleante:
![](https://github.com/DEVastationStudio/UnFair/blob/main/ReadmeImages/Unfair_Ball_Maze.png)

Concept del puesto del autómata pitonisa:
![](https://github.com/DEVastationStudio/UnFair/blob/main/ReadmeImages/Unfair_fortune_teller.png)

Concept art del puesto del tiro al pato:
![](https://github.com/DEVastationStudio/UnFair/blob/main/ReadmeImages/Unfair_tiro_al_pato1.png)
![](https://github.com/DEVastationStudio/UnFair/blob/main/ReadmeImages/Unfair_tiro_al_pato2.png)

### Sprites de Personajes
Pillo y señor loco:
![](https://github.com/DEVastationStudio/UnFair/blob/main/ReadmeImages/Unfair_pillo.png)
![](https://github.com/DEVastationStudio/UnFair/blob/main/ReadmeImages/Unfair_loco.png)

Ancianos:
![](https://github.com/DEVastationStudio/UnFair/blob/main/ReadmeImages/Unfair_abuela.png)
![](https://github.com/DEVastationStudio/UnFair/blob/main/ReadmeImages/Unfair_abuelo.png)
## Arte 3D
Los modelos 3D serán lowpoly en el escenario grande, de forma que no cargue demasiado la escena. Las escenas de cada minijuego contendrán una mezcla de modelos lowpoly y highpoly. Todos los modelos estarán texturizados y se usarán mapas de normales para dar más detalle. La textura predominante será la de madera pues la mayoría de elementos están construidos con este material.
* Atracciones

La atracción principal es la noria, está situada al fondo de la feria. Su tamaño es significativamente mayor al resto de elementos. Su diseño es simple y los asientos no están cubiertos. Su modelo destaca además por poseer un material metálico, en contraste con la madera predominante.

Hay varios tipos de modelos para los puestos diferentes que se repiten por toda la feria. Para diferenciarlos se usan colores diferentes en las texturas.

* Elementos decorativos

Con el objetivo de ambientar el escenario se añadirán elementos no interactuables como cajas, cubos, carteles y botellas. En los minijuegos se podrán ver al fondo los premios que se ofrecen en los puestos. Todos estos elementos sirven además para eliminar los espacios vacíos y dar vida al escenario.
* Assets de los minijuegos

Los elementos de los minijuegos deberán ser lowpoly para asegurar la fluidez y el buen funcionamiento del mismo. Aquellos modelos que necesiten de un mayor nivel de detalle se crearán en highpoly para luego ir bajando el número de polígonos con la herramienta Decimate de Blender pero manteniendo el Shade Smooth para que no se note en exceso la pérdida de calidad.
### Concept art
![](https://github.com/DEVastationStudio/UnFair/blob/main/ReadmeImages/ImaginaCortinas.png)
![](https://github.com/DEVastationStudio/UnFair/blob/main/ReadmeImages/puesto.png)
![](https://github.com/DEVastationStudio/UnFair/blob/main/ReadmeImages/PuestoTicketsNoria.png)
![](https://github.com/DEVastationStudio/UnFair/blob/main/ReadmeImages/puestoJuguetes.png)
![](https://github.com/DEVastationStudio/UnFair/blob/main/ReadmeImages/feriaCompleto.png)
# Sonido
## Banda sonora
Un\*fair contará con la siguiente música:

* Música del menú principal
* Música de la cutscene inicial
* Música de la feria
* Música del tiro al blanco
* Música de los caballos
* Músicade las canicas
* Música de victoria en un minijuego
* Música de derrota en un minijuego
* Música de la cutscene final
## Efectos de sonido
Los efectos de sonido que aparecerán en Un*fair son los siguientes:
* Sonido que suena cuando aparece el logo de DEVastation
* Sonido que sonará mientras un personaje hable
* Sonido de fin de minijuego (general)
* Patos:
  * Sonido de pescar un pato
  * Sonido de conseguir puntos
  * Sonido de perder puntos
* Tiro al blanco:
  * Sonido de disparar
  * Sonido de darle a la diana
  * Sonido de la diana dorada
  * Sonido de la diana de tiempo
  * Sonido de cuando queda poco tiempo
* Caballos:
  * Sonido de movimiento
  * Sonido de acertar el combo
  * Sonido de fallar el combo
* Canicas:
  * Sonido de lanzar canica
  * Sonido de chocar
  * Sonido de encestar
# Interfaz
Haciendo uso de las leyes del diseño de UX se quiere cumplir unos objetivos que aplicar a la UI.

* Los usuarios deben de poder acceder a toda la información en 3 o menos pasos para hacer una navegación más ligera.
* El usuario debe de poder trabajar a un ritmo rápido y moverse por la interfaz (Doherty Threshold: https://lawsofux.com/doherty-threshold/)
* El número de opciones que el usuario tiene a mano en cada momento debe de ser limitado (Miller’s Law: https://lawsofux.com/millers-law/) 

Más links de referencia: 
* https://uxdesign.cc/ux-and-video-game-design-5d8bcc50be67

## Menú principal (Extradiegética)
![](https://github.com/DEVastationStudio/UnFair/blob/main/ReadmeImages/menuPrincipal.png)

Elementos:
1. Título del juego
2. Botón de empezar el juego
3. Botón de salir del juego
4. Botón de ajustes
5. Botón de cutscenes desbloqueadas

* 5: Al pasarte el juego se puede añadir un botón donde poder volver a ver los finales que has desbloqueado y se muestran los finales no desbloqueados oscurecidos para dar pie a que el jugador vuelva a pasarse el juego. Habrá que pensar desde diseño como no hacer el juego muy repetitivo (Por ejemplo añadiendo diálogos con varios caminos)

* Cuando se pulsa el botón de empezar se desvanece la interfaz y cae al suelo de la feria donde apunta al jugador en la entrada.

## Menú de pausa (Diegética)
![](https://github.com/DEVastationStudio/UnFair/blob/main/ReadmeImages/mapaParqueUnfair.jpg)

Elementos:
1. Mapa de la feria con pegatinas que muestran el progreso de los minijuegos
2. Botón de salir al menú principal
3. Botón de ajustes para cambiar el volumen de la música y los efectos de sonido (Se pueden añadir más elementos configurables)
4. Botón de cerrar el menú de pausa aunque se pueda cerrar directamente pulsando esc/start. 

*Los elementos del 2 -> 4 pueden variar

El mapa tiene que quedar como si Diane lo estuviese abriendo y formase parte del mundo del juego. Los ajustes se encontrarán detrás del mapa, por lo que cuando pulsa ajustes el mapa se gira.

Las estrellas que Diane vaya consiguiendo se irán añadiendo en formato de pegatinas con 1, 2 o 3 estrellas. La noria saldrá oscurecida o con un cartel de fuera de servicio para mostrar al jugador que aún no puede acceder a ella. Esto último se puede cambiar y no dar feedback desde el mapa si no con el diálogo que el jugador lee cuando habla con el feriante encargado de la noria.

Debería de haber elementos en el mapa que representen las opciones del menú que se irán agrandando si la opción está seleccionada.

## Menú de Ajustes (Ambos)

El menú de ajustes se mostrará en 3 casos: Desde el menú principal, desde el menú de pausa de la feria y desde dentro del minijuego (Menú principal y pausa del minijuego)

La distribución de los ajustes será idéntica en todos los casos, lo único que cambiará será el fondo de los ajustes

![](https://github.com/DEVastationStudio/UnFair/blob/main/ReadmeImages/menuDeAjustes.jpg)

Elementos:
1. Pestañas de opciones
2. Sliders o botones de los parámetros que se quieran ajustar
3. Botón de salir de ajustes

Las pestañas de opciones solo se activan cuando se pulsa el botón de pestaña correspondiente. Desde los botones siempre se va a poder acceder a la pestaña seleccionada en el momento dando hacia abajo.

### Fase preGame (1)

El feriante preguntará si quieres echar una partida a lo que el jugador puede responder 3 cosas:
1. Si, quiero jugar
2. Mejor más tarde

Una vez se elige la opción 1 el jugador entra a la escena del minijuego y se le presenta una pantalla donde puede ver como conseguir las estrellas a parte de dos botones, empezar y salir. 

### Fase inGame (2)
![](https://github.com/DEVastationStudio/UnFair/blob/main/ReadmeImages/faseInGame.jpg)

Elementos:
1. Tablón que muestra datos relevantes de la partida
2. Minijuego
* A parte de estos dos elementos base cada minijuego puede añadir cosas extra necesarias para su minijuego en especial.

Una vez se ha dado a empezar en la fase preGame se activa una cuenta atrás de 3 segundos antes de poder comenzar a mover la mira.

### Fase post Game (3)

El feriante resume la partida y hace entrega de las pegatinas correspondientes, pudiendo el diálogo cambiar dependiendo de las condiciones: ha conseguido superar el antiguo récord, ha conseguido más estrellas de las que tenía, ha conseguido las 3 estrellas de golpe, etc…

Una vez ha concluido el resumen se vuelve a la fase 1 pero el diálogo cambia para dar consciencia al feriante de que vas a rejugar y que no parezca que repite el diálogo inicial.

#### Pej:

- ¿Quieres volver a jugar?
* ¡Vamos a por otra!
* Puedes explicarme las normas, que no me he enterado bien
* Ya he jugado mucho por hoy
