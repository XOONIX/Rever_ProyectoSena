„
UC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\WeatherForecast.cs
	namespace 	
rever
 
{ 
public 

class 
WeatherForecast  
{ 
public 
DateOnly 
Date 
{ 
get "
;" #
set$ '
;' (
}) *
public 
int 
TemperatureC 
{  !
get" %
;% &
set' *
;* +
}, -
public		 
int		 
TemperatureF		 
=>		  "
$num		# %
+		& '
(		( )
int		) ,
)		, -
(		- .
TemperatureC		. :
/		; <
$num		= C
)		C D
;		D E
public 
string 
? 
Summary 
{  
get! $
;$ %
set& )
;) *
}+ ,
} 
} ¥*
dC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Repositories\UsuarioRepository.cs
	namespace 	
rever
 
. 
Repositories 
{		 
public

 

class

 
UsuarioRepository

 "
:

# $
IUsuarioRepository

% 7
{ 
private 
readonly 
DatabaseService (
_context) 1
;1 2
public 
UsuarioRepository  
(  !
DatabaseService! 0
context1 8
)8 9
{ 	
this 
. 
_context 
= 
context #
;# $
} 	
public 
async 
Task 
< 
List 
< 
Usuario &
>& '
>' (

GetUsuario) 3
(3 4
)4 5
{ 	
var 
data 
= 
await 
_context %
.% &
Usuario& -
.- .
ToListAsync. 9
(9 :
): ;
;; <
return 
data 
; 
} 	
public 
async 
Task 
< 
Usuario !
>! "
GetUsuarioById# 1
(1 2
int2 5
id6 8
)8 9
{ 	
var 
data 
= 
await 
_context %
.% &
Usuario& -
.- .
FirstOrDefaultAsync. A
(A B
xB C
=>D F
xG H
.H I
	IdUsuarioI R
==S U
idV X
)X Y
;Y Z
return 
data 
; 
} 	
public 
async 
Task 
< 
Usuario !
?! "
>" #"
GetByEmailWithRolAsync$ :
(: ;
string; A
correoB H
)H I
{   	
return!! 
await!! 
_context!! !
.!!! "
Usuario!!" )
.!!) *
Include!!* 1
(!!1 2
u!!2 3
=>!!4 6
u!!7 8
.!!8 9
Rol!!9 <
)!!< =
.!!= >
FirstOrDefaultAsync!!> Q
(!!Q R
u!!R S
=>!!T V
u!!W X
.!!X Y
Correo!!Y _
==!!` b
correo!!c i
)!!i j
;!!j k
}"" 	
public$$ 
async$$ 
Task$$ 
<$$ 
bool$$ 
>$$ 
PostUsuario$$  +
($$+ ,
Usuario$$, 3
usuario$$4 ;
)$$; <
{%% 	
await&& 
_context&& 
.&& 
Usuario&& "
.&&" #
AddAsync&&# +
(&&+ ,
usuario&&, 3
)&&3 4
;&&4 5
await'' 
_context'' 
.'' 
SaveChangesAsync'' +
(''+ ,
)'', -
;''- .
return(( 
true(( 
;(( 
})) 	
public++ 
async++ 
Task++ 
<++ 
bool++ 
>++ 

PutUsuario++  *
(++* +
Usuario+++ 2
usuario++3 :
)++: ;
{,, 	
var-- 
exist-- 
=-- 
await-- 
_context-- &
.--& '
Usuario--' .
.--. /
FirstOrDefaultAsync--/ B
(--B C
x--C D
=>--E G
x--H I
.--I J
	IdUsuario--J S
==--T V
usuario--W ^
.--^ _
	IdUsuario--_ h
)--h i
;--i j
if.. 
(.. 
exist.. 
==.. 
null.. 
).. 
{// 
return00 
false00 
;00 
}11 
exist33 
.33 
Nombre33 
=33 
usuario33 "
.33" #
Nombre33# )
;33) *
exist44 
.44 
Correo44 
=44 
usuario44 "
.44" #
Correo44# )
;44) *
exist55 
.55 
Contrase√±a55 
=55 
usuario55 &
.55& '
Contrase√±a55' 1
;551 2
exist66 
.66 
Telefono66 
=66 
usuario66 $
.66$ %
Telefono66% -
;66- .
exist77 
.77 
IdRol77 
=77 
usuario77 !
.77! "
IdRol77" '
;77' (
await99 
_context99 
.99 
SaveChangesAsync99 +
(99+ ,
)99, -
;99- .
return:: 
true:: 
;:: 
};; 	
public== 
async== 
Task== 
<== 
bool== 
>== 
DeleteUsuario==  -
(==- .
Usuario==. 5
usuario==6 =
)=== >
{>> 	
_context?? 
.?? 
Usuario?? 
.?? 
Remove?? #
(??# $
usuario??$ +
)??+ ,
;??, -
await@@ 
_context@@ 
.@@ 
SaveChangesAsync@@ +
(@@+ ,
)@@, -
;@@- .
returnAA 
trueAA 
;AA 
}BB 	
}CC 
}DD º"
iC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Repositories\TipoInmuebleRepository.cs
	namespace 	
rever
 
. 
Repositories 
{		 
public

 

class

 "
TipoInmuebleRepository

 '
:

( )#
ITipoInmuebleRepository

* A
{ 
private 
readonly 
DatabaseService (
_context) 1
;1 2
public "
TipoInmuebleRepository %
(% &
DatabaseService& 5
context6 =
)= >
{ 	
this 
. 
_context 
= 
context "
;" #
} 	
public 
async 
Task 
< 
List 
< 
TipoInmueble +
>+ ,
>, -
GetTipoInmueble. =
(= >
)> ?
{ 	
var 
data 
= 
await 
_context %
.% &
TipoInmueble& 2
.2 3
ToListAsync3 >
(> ?
)? @
;@ A
return 
data 
; 
} 	
public 
async 
Task 
< 
TipoInmueble &
>& '
GetTipoInmuebleById( ;
(; <
int< ?
id@ B
)B C
{ 	
var 
data 
= 
await 
_context %
.% &
TipoInmueble& 2
.2 3
FirstOrDefaultAsync3 F
(F G
xG H
=>I K
xL M
.M N
IdTipoN T
==U W
idX Z
)Z [
;[ \
return 
data 
; 
} 	
public 
async 
Task 
< 
bool 
> 
PostTipoInmueble  0
(0 1
TipoInmueble1 =
tipoInmueble> J
)J K
{   	
await!! 
_context!! 
.!! 
TipoInmueble!! '
.!!' (
AddAsync!!( 0
(!!0 1
tipoInmueble!!1 =
)!!= >
;!!> ?
await"" 
_context"" 
."" 
SaveChangesAsync"" +
(""+ ,
)"", -
;""- .
return## 
true## 
;## 
}$$ 	
public&& 
async&& 
Task&& 
<&& 
bool&& 
>&& 
PutTipoInmueble&&  /
(&&/ 0
TipoInmueble&&0 <
tipoInmueble&&= I
)&&I J
{'' 	
var(( 
exist(( 
=(( 
_context((  
.((  !
TipoInmueble((! -
.((- .
FirstOrDefault((. <
(((< =
x((= >
=>((? A
x((B C
.((C D
IdTipo((D J
==((K M
tipoInmueble((N Z
.((Z [
IdTipo(([ a
)((a b
;((b c
if)) 
()) 
exist)) 
==)) 
null)) 
))) 
{** 
return++ 
false++ 
;++ 
},, 
exist.. 
... 
Nombre.. 
=.. 
tipoInmueble.. '
...' (
Nombre..( .
;... /
_context// 
.// 
TipoInmueble// !
.//! "
Update//" (
(//( )
tipoInmueble//) 5
)//5 6
;//6 7
await00 
_context00 
.00 
SaveChangesAsync00 +
(00+ ,
)00, -
;00- .
return11 
true11 
;11 
}22 	
public44 
async44 
Task44 
<44 
bool44 
>44 
DeleteTipoInmueble44  2
(442 3
TipoInmueble443 ?
tipoInmueble44@ L
)44L M
{55 	
_context66 
.66 
TipoInmueble66 !
.66! "
Remove66" (
(66( )
tipoInmueble66) 5
)665 6
;666 7
await77 
_context77 
.77 
SaveChangesAsync77 +
(77+ ,
)77, -
;77- .
return88 
true88 
;88 
}99 	
}:: 
};; ≤
lC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Repositories\ModoTransaccionRepository.cs
	namespace		 	
rever		
 
.		 
Repositories		 
{

 
public 

class %
ModoTransaccionRepository *
:+ ,&
IModoTransaccionRepository- G
{ 
private 
readonly 
DatabaseService (
_context) 1
;1 2
public %
ModoTransaccionRepository (
(( )
DatabaseService) 8
context9 @
)@ A
{ 	
_context 
= 
context 
; 
} 	
public 
async 
Task 
< 
IEnumerable %
<% &
ModoTransaccion& 5
>5 6
>6 7
GetAllAsync8 C
(C D
)D E
{ 	
return 
await 
_context !
.! "
ModoTransaccion" 1
.1 2
ToListAsync2 =
(= >
)> ?
;? @
} 	
public 
async 
Task 
< 
ModoTransaccion )
>) *
GetByIdAsync+ 7
(7 8
int8 ;
id< >
)> ?
{ 	
return 
await 
_context !
.! "
ModoTransaccion" 1
.1 2
	FindAsync2 ;
(; <
id< >
)> ?
;? @
} 	
public 
async 
Task 
< 
ModoTransaccion )
>) *
CreateAsync+ 6
(6 7
ModoTransaccion7 F
modoG K
)K L
{ 	
_context   
.   
ModoTransaccion   $
.  $ %
Add  % (
(  ( )
modo  ) -
)  - .
;  . /
await!! 
_context!! 
.!! 
SaveChangesAsync!! +
(!!+ ,
)!!, -
;!!- .
return"" 
modo"" 
;"" 
}## 	
public%% 
async%% 
Task%% 
<%% 
ModoTransaccion%% )
>%%) *
UpdateAsync%%+ 6
(%%6 7
ModoTransaccion%%7 F
modo%%G K
)%%K L
{&& 	
_context'' 
.'' 
ModoTransaccion'' $
.''$ %
Update''% +
(''+ ,
modo'', 0
)''0 1
;''1 2
await(( 
_context(( 
.(( 
SaveChangesAsync(( +
(((+ ,
)((, -
;((- .
return)) 
modo)) 
;)) 
}** 	
public,, 
async,, 
Task,, 
<,, 
bool,, 
>,, 
DeleteAsync,,  +
(,,+ ,
int,,, /
id,,0 2
),,2 3
{-- 	
var.. 
modo.. 
=.. 
await.. 
_context.. %
...% &
ModoTransaccion..& 5
...5 6
	FindAsync..6 ?
(..? @
id..@ B
)..B C
;..C D
if// 
(// 
modo// 
==// 
null// 
)// 
return// $
false//% *
;//* +
_context11 
.11 
ModoTransaccion11 $
.11$ %
Remove11% +
(11+ ,
modo11, 0
)110 1
;111 2
await22 
_context22 
.22 
SaveChangesAsync22 +
(22+ ,
)22, -
;22- .
return33 
true33 
;33 
}44 	
}55 
}66 Ω 
`C:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Repositories\RolRepository.cs
	namespace 	
rever
 
. 
Repositories 
{		 
public

 

class

 
RolRepository

 
:

  
IRolRepository

! /
{ 
private 
readonly 
DatabaseService (
_context) 1
;1 2
public 
RolRepository 
( 
DatabaseService ,
context- 4
)4 5
{ 	
this 
. 
_context 
= 
context "
;" #
} 	
public 
async 
Task 
< 
List 
< 
Rol "
>" #
># $
GetRol% +
(+ ,
), -
{ 	
var 
data 
= 
await 
_context %
.% &
Rol& )
.) *
ToListAsync* 5
(5 6
)6 7
;7 8
return 
data 
; 
} 	
public 
async 
Task 
< 
Rol 
> 

GetRolById )
() *
int* -
id. 0
)0 1
{ 	
var 
data 
= 
await 
_context %
.% &
Rol& )
.) *
FirstOrDefaultAsync* =
(= >
x> ?
=>@ B
xC D
.D E
IdRolE J
==K M
idN P
)P Q
;Q R
return 
data 
; 
} 	
public 
async 
Task 
< 
bool 
> 
PostRol  '
(' (
Rol( +
rol, /
)/ 0
{   	
await!! 
_context!! 
.!! 
Rol!! 
.!! 
AddAsync!! '
(!!' (
rol!!( +
)!!+ ,
;!!, -
await"" 
_context"" 
."" 
SaveChangesAsync"" +
(""+ ,
)"", -
;""- .
return## 
true## 
;## 
}$$ 	
public&& 
async&& 
Task&& 
<&& 
bool&& 
>&& 
PutRol&&  &
(&&& '
Rol&&' *
rol&&+ .
)&&. /
{'' 	
var(( 
exist(( 
=(( 
_context((  
.((  !
Rol((! $
.(($ %
FirstOrDefault((% 3
(((3 4
x((4 5
=>((6 8
x((9 :
.((: ;
IdRol((; @
==((A C
rol((D G
.((G H
IdRol((H M
)((M N
;((N O
if)) 
()) 
exist)) 
==)) 
null)) 
))) 
{** 
return++ 
false++ 
;++ 
},, 
exist.. 
... 
Nombre.. 
=.. 
rol.. 
... 
Nombre.. %
;..% &
_context// 
.// 
Rol// 
.// 
Update// 
(//  
rol//  #
)//# $
;//$ %
await00 
_context00 
.00 
SaveChangesAsync00 +
(00+ ,
)00, -
;00- .
return11 
true11 
;11 
}22 	
public33 
async33 
Task33 
<33 
bool33 
>33 
	DeleteRol33  )
(33) *
Rol33* -
rol33. 1
)331 2
{44 	
_context55 
.55 
Rol55 
.55 
Remove55 
(55  
rol55  #
)55# $
;55$ %
await66 
_context66 
.66 
SaveChangesAsync66 +
(66+ ,
)66, -
;66- .
return77 
true77 
;77 
}88 	
}99 
}:: Å"
fC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Repositories\LocalidadRepository.cs
	namespace		 	
rever		
 
.		 
Repositories		 
{

 
public 

class 
LocalidadRepository $
:% & 
ILocalidadRepository' ;
{ 
private 
readonly 
DatabaseService (
_context) 1
;1 2
public 
LocalidadRepository "
(" #
DatabaseService# 2
context3 :
): ;
{ 	
this 
. 
_context 
= 
context "
;" #
} 	
public 
async 
Task 
< 
List 
< 
	Localidad (
>( )
>) *
GetLocalidad+ 7
(7 8
)8 9
{ 	
var 
data 
= 
await 
_context %
.% &
	Localidad& /
./ 0
ToListAsync0 ;
(; <
)< =
;= >
return 
data 
; 
} 	
public 
async 
Task 
< 
	Localidad #
># $
GetLocalidadById% 5
(5 6
int6 9
id: <
)< =
{ 	
var 
data 
= 
await 
_context %
.% &
	Localidad& /
./ 0
FirstOrDefaultAsync0 C
(C D
xD E
=>F H
xI J
.J K
IdLocalidadK V
==W Y
idZ \
)\ ]
;] ^
return 
data 
; 
} 	
public   
async   
Task   
<   
bool   
>   
PostLocalidad    -
(  - .
	Localidad  . 7
	localidad  8 A
)  A B
{!! 	
await"" 
_context"" 
."" 
	Localidad"" $
.""$ %
AddAsync""% -
(""- .
	localidad"". 7
)""7 8
;""8 9
await## 
_context## 
.## 
SaveChangesAsync## +
(##+ ,
)##, -
;##- .
return$$ 
true$$ 
;$$ 
}%% 	
public'' 
async'' 
Task'' 
<'' 
bool'' 
>'' 
PutLocalidad''  ,
('', -
	Localidad''- 6
	localidad''7 @
)''@ A
{(( 	
var)) 
exist)) 
=)) 
_context))  
.))  !
	Localidad))! *
.))* +
FirstOrDefault))+ 9
())9 :
x)): ;
=>))< >
x))? @
.))@ A
IdLocalidad))A L
==))M O
	localidad))P Y
.))Y Z
IdLocalidad))Z e
)))e f
;))f g
if** 
(** 
exist** 
==** 
null** 
)** 
{++ 
return,, 
false,, 
;,, 
}-- 
exist// 
.// 
IdLocalidad// 
=// 
	localidad//  )
.//) *
IdLocalidad//* 5
;//5 6
_context00 
.00 
	Localidad00 
.00 
Update00 %
(00% &
	localidad00& /
)00/ 0
;000 1
await11 
_context11 
.11 
SaveChangesAsync11 +
(11+ ,
)11, -
;11- .
return22 
true22 
;22 
}33 	
public55 
async55 
Task55 
<55 
bool55 
>55 
DeleteLocalidad55  /
(55/ 0
	Localidad550 9
	localidad55: C
)55C D
{66 	
_context77 
.77 
	Localidad77 
.77 
Remove77 %
(77% &
	localidad77& /
)77/ 0
;770 1
await88 
_context88 
.88 
SaveChangesAsync88 +
(88+ ,
)88, -
;88- .
return99 
true99 
;99 
}:: 	
};; 
}<< ∫
pC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Repositories\interfaces\IUsuarioRepository.cs
	namespace 	
rever
 
. 
Repositories 
. 

Interfaces '
{ 
public 

	interface 
IUsuarioRepository '
{ 
Task		 
<		 
List		 
<		 
Usuario		 
>		 
>		 

GetUsuario		 &
(		& '
)		' (
;		( )
Task

 
<

 
Usuario

 
>

 
GetUsuarioById

 $
(

$ %
int

% (
id

) +
)

+ ,
;

, -
Task 
< 
Usuario 
? 
> "
GetByEmailWithRolAsync -
(- .
string. 4
correo5 ;
); <
;< =
Task 
< 
bool 
> 
PostUsuario 
( 
Usuario &
usuario' .
). /
;/ 0
Task 
< 
bool 
> 

PutUsuario 
( 
Usuario %
usuario& -
)- .
;. /
Task 
< 
bool 
> 
DeleteUsuario  
(  !
Usuario! (
usuario) 0
)0 1
;1 2
} 
} ∏

uC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Repositories\interfaces\ITipoInmuebleRepository.cs
	namespace 	
rever
 
. 
Repositories 
. 

Interfaces '
{ 
public 

	interface #
ITipoInmuebleRepository ,
{ 
Task		 
<		 
List		 
<		 
TipoInmueble		 
>		 
>		  
GetTipoInmueble		! 0
(		0 1
)		1 2
;		2 3
Task

 
<

 
TipoInmueble

 
>

 
GetTipoInmuebleById

 .
(

. /
int

/ 2
id

3 5
)

5 6
;

6 7
Task 
< 
bool 
> 
PostTipoInmueble #
(# $
TipoInmueble$ 0
tipoInmueble1 =
)= >
;> ?
Task 
< 
bool 
> 
PutTipoInmueble "
(" #
TipoInmueble# /
tipoInmueble0 <
)< =
;= >
Task 
< 
bool 
> 
DeleteTipoInmueble %
(% &
TipoInmueble& 2
tipoInmueble3 ?
)? @
;@ A
} 
} ±	
lC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Repositories\interfaces\IRolRepository.cs
	namespace 	
rever
 
. 
Repositories 
. 

Interfaces '
{ 
public 

	interface 
IRolRepository #
{ 
Task		 
<		 
List		 
<		 
Rol		 
>		 
>		 
GetRol		 
(		 
)		  
;		  !
Task

 
<

 
Rol

 
>

 

GetRolById

 
(

 
int

  
id

! #
)

# $
;

$ %
Task 
< 
bool 
> 
PostRol 
( 
Rol 
rol "
)" #
;# $
Task 
< 
bool 
> 
PutRol 
( 
Rol 
rol !
)! "
;" #
Task 
< 
bool 
> 
	DeleteRol 
( 
Rol  
rol! $
)$ %
;% &
} 
} ©

xC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Repositories\interfaces\IModoTransaccionRepository.cs
	namespace 	
rever
 
. 
Repositories 
. 

Interfaces '
{ 
public 

	interface &
IModoTransaccionRepository /
{ 
Task		 
<		 
IEnumerable		 
<		 
ModoTransaccion		 (
>		( )
>		) *
GetAllAsync		+ 6
(		6 7
)		7 8
;		8 9
Task

 
<

 
ModoTransaccion

 
>

 
GetByIdAsync

 *
(

* +
int

+ .
id

/ 1
)

1 2
;

2 3
Task 
< 
ModoTransaccion 
> 
CreateAsync )
() *
ModoTransaccion* 9
modo: >
)> ?
;? @
Task 
< 
ModoTransaccion 
> 
UpdateAsync )
() *
ModoTransaccion* 9
modo: >
)> ?
;? @
Task 
< 
bool 
> 
DeleteAsync 
( 
int "
id# %
)% &
;& '
} 
} ã

rC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Repositories\interfaces\ILocalidadRepository.cs
	namespace 	
rever
 
. 
Repositories 
. 

Interfaces '
{ 
public 

	interface  
ILocalidadRepository )
{ 
Task		 
<		 
List		 
<		 
	Localidad		 
>		 
>		 
GetLocalidad		 *
(		* +
)		+ ,
;		, -
Task

 
<

 
	Localidad

 
>

 
GetLocalidadById

 (
(

( )
int

) ,
id

- /
)

/ 0
;

0 1
Task 
< 
bool 
> 
PostLocalidad  
(  !
	Localidad! *
	localidad+ 4
)4 5
;5 6
Task 
< 
bool 
> 
PutLocalidad 
(  
	Localidad  )
	localidad* 3
)3 4
;4 5
Task 
< 
bool 
> 
DeleteLocalidad "
(" #
	Localidad# ,
	localidad- 6
)6 7
;7 8
} 
} °
qC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Repositories\interfaces\IInmuebleRepository.cs
	namespace 	
rever
 
. 
Repositories 
. 

Interfaces '
{ 
public		 

	interface		 
IInmuebleRepository		 (
{

 
Task 
< 
List 
< 
Inmueble 
> 
> 
GetInmueble (
(( )
)) *
;* +
Task 
< 
Inmueble 
> 
GetInmuebleById &
(& '
int' *
id+ -
)- .
;. /
Task 
< 
bool 
> 
PostInmueble 
(  
Inmueble  (
inmueble) 1
)1 2
;2 3
Task 
< 
bool 
> 
PutInmueble 
( 
Inmueble '
inmueble( 0
)0 1
;1 2
Task 
< 
bool 
> 
DeleteInmueble !
(! "
Inmueble" *
inmueble+ 3
)3 4
;4 5
Task 
< 
IEnumerable 
< 
InmuebleListadoDto +
>+ ,
>, -
GetListadoAsync. =
(= >
)> ?
;? @
Task 
< 
InmuebleDetalleDto 
?  
>  !
GetDetalleAsync" 1
(1 2
int2 5
id6 8
)8 9
;9 :
Task 
< 
Inmueble 
? 
>  
PostInmuebleCompleto ,
(, -
CrearInmuebleDto- =
dto> A
,A B
intC F
	idUsuarioG P
)P Q
;Q R
} 
} °

C:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Repositories\interfaces\IInmuebleCaracteristicaRepository.cs
	namespace 	
rever
 
. 
Repositories 
. 

Interfaces '
{ 
public 

	interface -
!IInmuebleCaracteristicaRepository 6
{ 
Task		 
<		 
List		 
<		 "
InmuebleCaracteristica		 (
>		( )
>		) *%
GetInmuebleCaracteristica		+ D
(		D E
)		E F
;		F G
Task

 
<

 "
InmuebleCaracteristica

 #
>

# $
GetByIds

% -
(

- .
int

. 1

idInmueble

2 <
,

< =
int

> A
idCaracteristica

B R
)

R S
;

S T
Task 
< 
bool 
> &
PostInmuebleCaracteristica -
(- ."
InmuebleCaracteristica. D"
inmuebleCaracteristicaE [
)[ \
;\ ]
Task 
< 
bool 
> (
DeleteInmuebleCaracteristica /
(/ 0"
InmuebleCaracteristica0 F"
inmuebleCaracteristicaG ]
)] ^
;^ _
} 
} ﬁ	
oC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Repositories\interfaces\IImagenRepository.cs
	namespace 	
rever
 
. 
Repositories 
. 

Interfaces '
{ 
public 

	interface 
IImagenRepository &
{ 
Task		 
<		 
List		 
<		 
Imagen		 
>		 
>		 
	GetImagen		 $
(		$ %
)		% &
;		& '
Task

 
<

 
Imagen

 
>

 
GetImagenById

 "
(

" #
int

# &
id

' )
)

) *
;

* +
Task 
< 
bool 
> 

PostImagen 
( 
Imagen $
imagen% +
)+ ,
;, -
Task 
< 
bool 
> 
	PutImagen 
( 
Imagen #
imagen$ *
)* +
;+ ,
Task 
< 
bool 
> 
DeleteImagen 
(  
Imagen  &
imagen' -
)- .
;. /
} 
} É
zC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Repositories\interfaces\IEstadoPublicacionRepository.cs
	namespace 	
rever
 
. 
Repositories 
. 

Interfaces '
{ 
public 

	interface (
IEstadoPublicacionRepository 1
{ 
Task		 
<		 
List		 
<		 
EstadoPublicacion		 #
>		# $
>		$ % 
GetEstadoPublicacion		& :
(		: ;
)		; <
;		< =
Task

 
<

 
EstadoPublicacion

 
>

 $
GetEstadoPublicacionById

  8
(

8 9
int

9 <
id

= ?
)

? @
;

@ A
Task 
< 
bool 
> !
PostEstadoPublicacion (
(( )
EstadoPublicacion) :
estadoPublicacion; L
)L M
;M N
Task 
< 
bool 
>  
PutEstadoPublicacion '
(' (
EstadoPublicacion( 9
estadoPublicacion: K
)K L
;L M
Task 
< 
bool 
> #
DeleteEstadoPublicacion *
(* +
EstadoPublicacion+ <
estadoPublicacion= N
)N O
;O P
} 
} ¸	
qC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Repositories\interfaces\IContactoRepository.cs
	namespace 	
rever
 
. 
Repositories 
. 

Interfaces '
{ 
public 

	interface 
IContactoRepository (
{ 
Task		 
<		 
List		 
<		 
Contacto		 
>		 
>		 
GetContacto		 (
(		( )
)		) *
;		* +
Task

 
<

 
Contacto

 
>

 
GetContactoById

 &
(

& '
int

' *
id

+ -
)

- .
;

. /
Task 
< 
bool 
> 
PostContacto 
(  
Contacto  (
contacto) 1
)1 2
;2 3
Task 
< 
bool 
> 
PutContacto 
( 
Contacto '
contacto( 0
)0 1
;1 2
Task 
< 
bool 
> 
DeleteContacto !
(! "
Contacto" *
contacto+ 3
)3 4
;4 5
} 
} ﬁ	
oC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Repositories\interfaces\ICiudadRepository.cs
	namespace 	
rever
 
. 
Repositories 
. 

Interfaces '
{ 
public 

	interface 
ICiudadRepository &
{ 
Task		 
<		 
List		 
<		 
Ciudad		 
>		 
>		 
	GetCiudad		 $
(		$ %
)		% &
;		& '
Task

 
<

 
Ciudad

 
>

 
GetCiudadById

 "
(

" #
int

# &
id

' )
)

) *
;

* +
Task 
< 
bool 
> 

PostCiudad 
( 
Ciudad $
ciudad% +
)+ ,
;, -
Task 
< 
bool 
> 
	PutCiudad 
( 
Ciudad #
ciudad$ *
)* +
;+ ,
Task 
< 
bool 
> 
DeleteCiudad 
(  
Ciudad  &
ciudad' -
)- .
;. /
} 
} ÷

wC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Repositories\interfaces\ICaracteristicaRepository.cs
	namespace 	
rever
 
. 
Repositories 
. 

Interfaces '
{ 
public 

	interface %
ICaracteristicaRepository .
{ 
Task		 
<		 
List		 
<		 
Caracteristica		  
>		  !
>		! "
GetCaracteristica		# 4
(		4 5
)		5 6
;		6 7
Task

 
<

 
Caracteristica

 
>

 !
GetCaracteristicaById

 2
(

2 3
int

3 6
id

7 9
)

9 :
;

: ;
Task 
< 
bool 
> 
PostCaracteristica %
(% &
Caracteristica& 4
caracteristica5 C
)C D
;D E
Task 
< 
bool 
> 
PutCaracteristica $
($ %
Caracteristica% 3
caracteristica4 B
)B C
;C D
Task 
< 
bool 
>  
DeleteCaracteristica '
(' (
Caracteristica( 6
caracteristica7 E
)E F
;F G
} 
} ·ƒ
eC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Repositories\InmuebleRepository.cs
	namespace 	
rever
 
. 
Repositories 
{ 
public 

class 
InmuebleRepository #
:$ %
IInmuebleRepository& 9
{ 
private 
readonly 
DatabaseService (
_context) 1
;1 2
public 
InmuebleRepository !
(! "
DatabaseService" 1
context2 9
)9 :
{ 	
this 
. 
_context 
= 
context #
;# $
} 	
public 
async 
Task 
< 
List 
< 
Inmueble '
>' (
>( )
GetInmueble* 5
(5 6
)6 7
{ 	
var 
data 
= 
await 
_context %
.% &
Inmueble& .
.. /
ToListAsync/ :
(: ;
); <
;< =
return 
data 
; 
} 	
public 
async 
Task 
< 
Inmueble "
>" #
GetInmuebleById$ 3
(3 4
int4 7
id8 :
): ;
{ 	
var 
data 
= 
await 
_context %
.% &
Inmueble& .
.. /
FirstOrDefaultAsync/ B
(B C
xC D
=>E G
xH I
.I J

IdInmuebleJ T
==U W
idX Z
)Z [
;[ \
return 
data 
; 
}   	
public"" 
async"" 
Task"" 
<"" 
bool"" 
>"" 
PostInmueble""  ,
("", -
Inmueble""- 5
Inmueble""6 >
)""> ?
{## 	
await$$ 
_context$$ 
.$$ 
Inmueble$$ #
.$$# $
AddAsync$$$ ,
($$, -
Inmueble$$- 5
)$$5 6
;$$6 7
await%% 
_context%% 
.%% 
SaveChangesAsync%% +
(%%+ ,
)%%, -
;%%- .
return&& 
true&& 
;&& 
}'' 	
public)) 
async)) 
Task)) 
<)) 
bool)) 
>)) 
PutInmueble))  +
())+ ,
Inmueble)), 4
Inmueble))5 =
)))= >
{** 	
var++ 
exist++ 
=++ 
_context++  
.++  !
Inmueble++! )
.++) *
FirstOrDefault++* 8
(++8 9
x++9 :
=>++; =
x++> ?
.++? @

IdInmueble++@ J
==++K M
Inmueble++N V
.++V W

IdInmueble++W a
)++a b
;++b c
if,, 
(,, 
exist,, 
==,, 
null,, 
),, 
{-- 
return.. 
false.. 
;.. 
}// 
exist11 
.11 
Titulo11 
=11 
Inmueble11 #
.11# $
Titulo11$ *
;11* +
exist22 
.22 
Descripcion22 
=22 
Inmueble22  (
.22( )
Descripcion22) 4
;224 5
exist33 
.33 
Precio33 
=33 
Inmueble33 #
.33# $
Precio33$ *
;33* +
exist44 
.44 
IdTipo44 
=44 
Inmueble44 #
.44# $
IdTipo44$ *
;44* +
exist55 
.55 
IdModo55 
=55 
Inmueble55 #
.55# $
IdModo55$ *
;55* +
exist66 
.66 
	Direccion66 
=66 
Inmueble66 &
.66& '
	Direccion66' 0
;660 1
exist77 
.77 
IdLocalidad77 
=77 
Inmueble77  (
.77( )
IdLocalidad77) 4
;774 5
exist88 
.88 
Habitaciones88 
=88  
Inmueble88! )
.88) *
Habitaciones88* 6
;886 7
exist99 
.99 
Ba√±os99 
=99 
Inmueble99 "
.99" #
Ba√±os99# (
;99( )
exist:: 
.:: 
MetrosCuadrados:: !
=::" #
Inmueble::$ ,
.::, -
MetrosCuadrados::- <
;::< =
exist;; 
.;; 
Estrato;; 
=;; 
Inmueble;; $
.;;$ %
Estrato;;% ,
;;;, -
exist<< 
.<< 
Pisos<< 
=<< 
Inmueble<< "
.<<" #
Pisos<<# (
;<<( )
exist== 
.== 
Latitud== 
=== 
Inmueble== $
.==$ %
Latitud==% ,
;==, -
exist>> 
.>> 
Longitud>> 
=>> 
Inmueble>> %
.>>% &
Longitud>>& .
;>>. /
exist?? 
.?? 
	IdUsuario?? 
=?? 
Inmueble?? &
.??& '
	IdUsuario??' 0
;??0 1
exist@@ 
.@@ 
IdEstado@@ 
=@@ 
Inmueble@@ %
.@@% &
IdEstado@@& .
;@@. /
_contextAA 
.AA 
InmuebleAA 
.AA 
UpdateAA $
(AA$ %
InmuebleAA% -
)AA- .
;AA. /
awaitBB 
_contextBB 
.BB 
SaveChangesAsyncBB +
(BB+ ,
)BB, -
;BB- .
returnCC 
trueCC 
;CC 
}DD 	
publicFF 
asyncFF 
TaskFF 
<FF 
boolFF 
>FF 
DeleteInmuebleFF  .
(FF. /
InmuebleFF/ 7
InmuebleFF8 @
)FF@ A
{GG 	
_contextHH 
.HH 
InmuebleHH 
.HH 
RemoveHH $
(HH$ %
InmuebleHH% -
)HH- .
;HH. /
awaitII 
_contextII 
.II 
SaveChangesAsyncII +
(II+ ,
)II, -
;II- .
returnJJ 
trueJJ 
;JJ 
}KK 	
publicOO 
asyncOO 
TaskOO 
<OO 
IEnumerableOO %
<OO% &
InmuebleListadoDtoOO& 8
>OO8 9
>OO9 :
GetListadoAsyncOO; J
(OOJ K
)OOK L
{PP 	
returnQQ 
awaitQQ 
_contextQQ !
.QQ! "
InmuebleQQ" *
.RR 
IncludeRR 
(RR 
iRR 
=>RR 
iRR 
.RR  
	LocalidadRR  )
)RR) *
.RR* +
ThenIncludeRR+ 6
(RR6 7
lRR7 8
=>RR9 ;
lRR< =
.RR= >
CiudadRR> D
)RRD E
.SS 
IncludeSS 
(SS 
iSS 
=>SS 
iSS 
.SS  
TipoInmuebleSS  ,
)SS, -
.TT 
IncludeTT 
(TT 
iTT 
=>TT 
iTT 
.TT  
ModoTransaccionTT  /
)TT/ 0
.UU 
IncludeUU 
(UU 
iUU 
=>UU 
iUU 
.UU  
ImagenesUU  (
)UU( )
.VV 
IncludeVV 
(VV 
iVV 
=>VV 
iVV 
.VV  #
InmuebleCaracteristicasVV  7
)VV7 8
.VV8 9
ThenIncludeVV9 D
(VVD E
icVVE G
=>VVH J
icVVK M
.VVM N
CaracteristicaVVN \
)VV\ ]
.WW 
SelectWW 
(WW 
iWW 
=>WW 
newWW  
InmuebleListadoDtoWW! 3
{XX 

IdInmuebleYY 
=YY  
iYY! "
.YY" #

IdInmuebleYY# -
,YY- .
TituloZZ 
=ZZ 
iZZ 
.ZZ 
TituloZZ %
,ZZ% &
Precio[[ 
=[[ 
i[[ 
.[[ 
Precio[[ %
,[[% &
	Ubicacion\\ 
=\\ 
i\\  !
.\\! "
	Localidad\\" +
.\\+ ,
Nombre\\, 2
+\\3 4
$str\\5 9
+\\: ;
i\\< =
.\\= >
	Localidad\\> G
.\\G H
Ciudad\\H N
.\\N O
Nombre\\O U
,\\U V
Habitaciones]]  
=]]! "
i]]# $
.]]$ %
Habitaciones]]% 1
,]]1 2
Banos^^ 
=^^ 
i^^ 
.^^ 
Ba√±os^^ #
,^^# $
MetrosCuadrados__ #
=__$ %
i__& '
.__' (
MetrosCuadrados__( 7
,__7 8
Pisos`` 
=`` 
i`` 
.`` 
Pisos`` #
,``# $
Tipoaa 
=aa 
iaa 
.aa 
TipoInmuebleaa )
.aa) *
Nombreaa* 0
,aa0 1
Modobb 
=bb 
ibb 
.bb 
ModoTransaccionbb ,
.bb, -
Nombrebb- 3
.bb3 4
ToLowerbb4 ;
(bb; <
)bb< =
,bb= >
Ciudadcc 
=cc 
icc 
.cc 
	Localidadcc (
.cc( )
Ciudadcc) /
.cc/ 0
Nombrecc0 6
,cc6 7
	Localidaddd 
=dd 
idd  !
.dd! "
	Localidaddd" +
.dd+ ,
Nombredd, 2
,dd2 3
	ImagenUrlee 
=ee 
iee  !
.ee! "
Imagenesee" *
.ee* +
Whereee+ 0
(ee0 1
imgee1 4
=>ee5 7
imgee8 ;
.ee; <
Portadaee< C
)eeC D
.eeD E
SelecteeE K
(eeK L
imgeeL O
=>eeP R
imgeeS V
.eeV W
UrleeW Z
)eeZ [
.ee[ \
FirstOrDefaultee\ j
(eej k
)eek l
??eem o
ieep q
.eeq r
Imageneseer z
.eez {
Select	ee{ Å
(
eeÅ Ç
img
eeÇ Ö
=>
eeÜ à
img
eeâ å
.
eeå ç
Url
eeç ê
)
eeê ë
.
eeë í
FirstOrDefault
eeí †
(
ee† °
)
ee° ¢
,
ee¢ £
Caracteristicasff #
=ff$ %
iff& '
.ff' (#
InmuebleCaracteristicasff( ?
.ff? @
Selectff@ F
(ffF G
icffG I
=>ffJ L
icffM O
.ffO P
CaracteristicaffP ^
.ff^ _
Nombreff_ e
)ffe f
.fff g
ToListffg m
(ffm n
)ffn o
,ffo p
}gg 
)gg 
.hh 
ToListAsynchh 
(hh 
)hh 
;hh 
}ii 	
publickk 
asynckk 
Taskkk 
<kk 
InmuebleDetalleDtokk ,
?kk, -
>kk- .
GetDetalleAsynckk/ >
(kk> ?
intkk? B
idkkC E
)kkE F
{ll 	
returnmm 
awaitmm 
_contextmm !
.mm! "
Inmueblemm" *
.nn 
Includenn 
(nn 
inn 
=>nn 
inn 
.nn  
	Localidadnn  )
)nn) *
.nn* +
ThenIncludenn+ 6
(nn6 7
lnn7 8
=>nn9 ;
lnn< =
.nn= >
Ciudadnn> D
)nnD E
.oo 
Includeoo 
(oo 
ioo 
=>oo 
ioo 
.oo  
TipoInmuebleoo  ,
)oo, -
.pp 
Includepp 
(pp 
ipp 
=>pp 
ipp 
.pp  
ModoTransaccionpp  /
)pp/ 0
.qq 
Includeqq 
(qq 
iqq 
=>qq 
iqq 
.qq  
Imagenesqq  (
)qq( )
.rr 
Includerr 
(rr 
irr 
=>rr 
irr 
.rr  #
InmuebleCaracteristicasrr  7
)rr7 8
.rr8 9
ThenIncluderr9 D
(rrD E
icrrE G
=>rrH J
icrrK M
.rrM N
CaracteristicarrN \
)rr\ ]
.ss 
Includess 
(ss 
iss 
=>ss 
iss 
.ss  
Usuarioss  '
)ss' (
.tt 
Wherett 
(tt 
itt 
=>tt 
itt 
.tt 

IdInmueblett (
==tt) +
idtt, .
)tt. /
.uu 
Selectuu 
(uu 
iuu 
=>uu 
newuu  
InmuebleDetalleDtouu! 3
{vv 

IdInmuebleww 
=ww  
iww! "
.ww" #

IdInmuebleww# -
,ww- .
Tituloxx 
=xx 
ixx 
.xx 
Tituloxx %
,xx% &
Descripcionyy 
=yy  !
iyy" #
.yy# $
Descripcionyy$ /
,yy/ 0
Preciozz 
=zz 
izz 
.zz 
Preciozz %
,zz% &
	Direccion{{ 
={{ 
i{{  !
.{{! "
	Direccion{{" +
,{{+ ,
	Ubicacion|| 
=|| 
i||  !
.||! "
	Localidad||" +
.||+ ,
Nombre||, 2
+||3 4
$str||5 9
+||: ;
i||< =
.||= >
	Localidad||> G
.||G H
Ciudad||H N
.||N O
Nombre||O U
,||U V
Habitaciones}}  
=}}! "
i}}# $
.}}$ %
Habitaciones}}% 1
,}}1 2
Banos~~ 
=~~ 
i~~ 
.~~ 
Ba√±os~~ #
,~~# $
MetrosCuadrados #
=$ %
i& '
.' (
MetrosCuadrados( 7
,7 8
Pisos
ÄÄ 
=
ÄÄ 
i
ÄÄ 
.
ÄÄ 
Pisos
ÄÄ #
,
ÄÄ# $
Estrato
ÅÅ 
=
ÅÅ 
i
ÅÅ 
.
ÅÅ  
Estrato
ÅÅ  '
,
ÅÅ' (
Latitud
ÇÇ 
=
ÇÇ 
i
ÇÇ 
.
ÇÇ  
Latitud
ÇÇ  '
,
ÇÇ' (
Longitud
ÉÉ 
=
ÉÉ 
i
ÉÉ  
.
ÉÉ  !
Longitud
ÉÉ! )
,
ÉÉ) *
Tipo
ÑÑ 
=
ÑÑ 
i
ÑÑ 
.
ÑÑ 
TipoInmueble
ÑÑ )
.
ÑÑ) *
Nombre
ÑÑ* 0
,
ÑÑ0 1
Modo
ÖÖ 
=
ÖÖ 
i
ÖÖ 
.
ÖÖ 
ModoTransaccion
ÖÖ ,
.
ÖÖ, -
Nombre
ÖÖ- 3
.
ÖÖ3 4
ToLower
ÖÖ4 ;
(
ÖÖ; <
)
ÖÖ< =
,
ÖÖ= >
Imagenes
ÜÜ 
=
ÜÜ 
i
ÜÜ  
.
ÜÜ  !
Imagenes
ÜÜ! )
.
ÜÜ) *
Select
ÜÜ* 0
(
ÜÜ0 1
img
ÜÜ1 4
=>
ÜÜ5 7
img
ÜÜ8 ;
.
ÜÜ; <
Url
ÜÜ< ?
)
ÜÜ? @
.
ÜÜ@ A
ToList
ÜÜA G
(
ÜÜG H
)
ÜÜH I
,
ÜÜI J
Caracteristicas
áá #
=
áá$ %
i
áá& '
.
áá' (%
InmuebleCaracteristicas
áá( ?
.
áá? @
Select
áá@ F
(
ááF G
ic
ááG I
=>
ááJ L
ic
ááM O
.
ááO P
Caracteristica
ááP ^
.
áá^ _
Nombre
áá_ e
)
ááe f
.
ááf g
ToList
áág m
(
áám n
)
áán o
,
ááo p

IdVendedor
àà 
=
àà  
i
àà! "
.
àà" #
	IdUsuario
àà# ,
,
àà, -
NombreVendedor
ââ "
=
ââ# $
i
ââ% &
.
ââ& '
Usuario
ââ' .
.
ââ. /
Nombre
ââ/ 5
,
ââ5 6
TelefonoVendedor
ää $
=
ää% &
i
ää' (
.
ää( )
Usuario
ää) 0
.
ää0 1
Telefono
ää1 9
,
ää9 :
CorreoVendedor
ãã "
=
ãã# $
i
ãã% &
.
ãã& '
Usuario
ãã' .
.
ãã. /
Correo
ãã/ 5
,
ãã5 6
}
åå 
)
åå 
.
çç !
FirstOrDefaultAsync
çç $
(
çç$ %
)
çç% &
;
çç& '
}
éé 	
public
êê 
async
êê 
Task
êê 
<
êê 
Inmueble
êê "
?
êê" #
>
êê# $"
PostInmuebleCompleto
êê% 9
(
êê9 :
CrearInmuebleDto
êê: J
dto
êêK N
,
êêN O
int
êêP S
	idUsuario
êêT ]
)
êê] ^
{
ëë 	
var
íí 
caracIds
íí 
=
íí 
dto
íí 
.
íí  
CaracteristicasIds
íí 1
.
íí1 2
Distinct
íí2 :
(
íí: ;
)
íí; <
.
íí< =
ToList
íí= C
(
ííC D
)
ííD E
;
ííE F
var
ïï 
localidadExiste
ïï 
=
ïï  !
await
ïï" '
_context
ïï( 0
.
ïï0 1
	Localidad
ïï1 :
.
ññ 
AnyAsync
ññ 
(
ññ 
l
ññ 
=>
ññ 
l
ññ  
.
ññ  !
IdLocalidad
ññ! ,
==
ññ- /
dto
ññ0 3
.
ññ3 4
IdLocalidad
ññ4 ?
)
ññ? @
;
ññ@ A
if
óó 
(
óó 
!
óó 
localidadExiste
óó  
)
óó  !
throw
òò 
new
òò 
ArgumentException
òò +
(
òò+ ,
$str
òò, R
)
òòR S
;
òòS T
if
õõ 
(
õõ 
caracIds
õõ 
.
õõ 
Count
õõ 
>
õõ  
$num
õõ! "
)
õõ" #
{
úú 
var
ùù 

existentes
ùù 
=
ùù  
await
ùù! &
_context
ùù' /
.
ùù/ 0
Caracteristica
ùù0 >
.
ûû 

CountAsync
ûû 
(
ûû  
c
ûû  !
=>
ûû" $
caracIds
ûû% -
.
ûû- .
Contains
ûû. 6
(
ûû6 7
c
ûû7 8
.
ûû8 9
IdCaracteristica
ûû9 I
)
ûûI J
)
ûûJ K
;
ûûK L
if
üü 
(
üü 

existentes
üü 
!=
üü !
caracIds
üü" *
.
üü* +
Count
üü+ 0
)
üü0 1
throw
†† 
new
†† 
ArgumentException
†† /
(
††/ 0
$str
††0 W
)
††W X
;
††X Y
}
°° 
var
££ 
strategy
££ 
=
££ 
_context
££ #
.
££# $
Database
££$ ,
.
££, -%
CreateExecutionStrategy
££- D
(
££D E
)
££E F
;
££F G
return
•• 
await
•• 
strategy
•• !
.
••! "
ExecuteAsync
••" .
(
••. /
async
••/ 4
(
••5 6
)
••6 7
=>
••8 :
{
¶¶ 
_context
®® 
.
®® 
ChangeTracker
®® &
.
®®& '
Clear
®®' ,
(
®®, -
)
®®- .
;
®®. /
await
™™ 
using
™™ 
var
™™ 
tx
™™  "
=
™™# $
await
™™% *
_context
™™+ 3
.
™™3 4
Database
™™4 <
.
™™< =#
BeginTransactionAsync
™™= R
(
™™R S
)
™™S T
;
™™T U
try
´´ 
{
¨¨ 
var
≠≠ 
inmueble
≠≠  
=
≠≠! "
new
≠≠# &
Inmueble
≠≠' /
{
ÆÆ 
Titulo
ØØ 
=
ØØ  
dto
ØØ! $
.
ØØ$ %
Titulo
ØØ% +
.
ØØ+ ,
Trim
ØØ, 0
(
ØØ0 1
)
ØØ1 2
,
ØØ2 3
Descripcion
∞∞ #
=
∞∞$ %
dto
∞∞& )
.
∞∞) *
Descripcion
∞∞* 5
.
∞∞5 6
Trim
∞∞6 :
(
∞∞: ;
)
∞∞; <
,
∞∞< =
Precio
±± 
=
±±  
dto
±±! $
.
±±$ %
Precio
±±% +
,
±±+ ,
IdTipo
≤≤ 
=
≤≤  
dto
≤≤! $
.
≤≤$ %
IdTipo
≤≤% +
,
≤≤+ ,
IdModo
≥≥ 
=
≥≥  
dto
≥≥! $
.
≥≥$ %
IdModo
≥≥% +
,
≥≥+ ,
	Direccion
¥¥ !
=
¥¥" #
dto
¥¥$ '
.
¥¥' (
	Direccion
¥¥( 1
.
¥¥1 2
Trim
¥¥2 6
(
¥¥6 7
)
¥¥7 8
,
¥¥8 9
IdLocalidad
µµ #
=
µµ$ %
dto
µµ& )
.
µµ) *
IdLocalidad
µµ* 5
,
µµ5 6
Habitaciones
∂∂ $
=
∂∂% &
dto
∂∂' *
.
∂∂* +
Habitaciones
∂∂+ 7
,
∂∂7 8
Ba√±os
∑∑ 
=
∑∑ 
dto
∑∑  #
.
∑∑# $
Banos
∑∑$ )
,
∑∑) *
Pisos
∏∏ 
=
∏∏ 
dto
∏∏  #
.
∏∏# $
Pisos
∏∏$ )
,
∏∏) *
MetrosCuadrados
ππ '
=
ππ( )
dto
ππ* -
.
ππ- .
MetrosCuadrados
ππ. =
,
ππ= >
Estrato
∫∫ 
=
∫∫  !
dto
∫∫" %
.
∫∫% &
Estrato
∫∫& -
,
∫∫- .
Latitud
ªª 
=
ªª  !
dto
ªª" %
.
ªª% &
Latitud
ªª& -
,
ªª- .
Longitud
ºº  
=
ºº! "
dto
ºº# &
.
ºº& '
Longitud
ºº' /
,
ºº/ 0
	IdUsuario
ΩΩ !
=
ΩΩ" #
	idUsuario
ΩΩ$ -
,
ΩΩ- .
IdEstado
ææ  
=
ææ! "
$num
ææ# $
,
ææ$ %
}
øø 
;
øø 
_context
¡¡ 
.
¡¡ 
Inmueble
¡¡ %
.
¡¡% &
Add
¡¡& )
(
¡¡) *
inmueble
¡¡* 2
)
¡¡2 3
;
¡¡3 4
await
¬¬ 
_context
¬¬ "
.
¬¬" #
SaveChangesAsync
¬¬# 3
(
¬¬3 4
)
¬¬4 5
;
¬¬5 6
if
ƒƒ 
(
ƒƒ 
caracIds
ƒƒ  
.
ƒƒ  !
Count
ƒƒ! &
>
ƒƒ' (
$num
ƒƒ) *
)
ƒƒ* +
{
≈≈ 
_context
∆∆  
.
∆∆  !$
InmuebleCaracteristica
∆∆! 7
.
∆∆7 8
AddRange
∆∆8 @
(
∆∆@ A
caracIds
«« $
.
««$ %
Select
««% +
(
««+ ,
id
««, .
=>
««/ 1
new
««2 5$
InmuebleCaracteristica
««6 L
{
»» 

IdInmueble
……  *
=
……+ ,
inmueble
……- 5
.
……5 6

IdInmueble
……6 @
,
……@ A
IdCaracteristica
    0
=
  1 2
id
  3 5
}
ÀÀ 
)
ÀÀ 
)
ÀÀ 
;
ÀÀ  
}
ÃÃ 
var
ŒŒ 
urls
ŒŒ 
=
ŒŒ 
dto
ŒŒ "
.
ŒŒ" #
ImagenesUrls
ŒŒ# /
.
œœ 
Where
œœ 
(
œœ 
u
œœ  
=>
œœ! #
!
œœ$ %
string
œœ% +
.
œœ+ , 
IsNullOrWhiteSpace
œœ, >
(
œœ> ?
u
œœ? @
)
œœ@ A
)
œœA B
.
–– 
Select
–– 
(
––  
u
––  !
=>
––" $
u
––% &
.
––& '
Trim
––' +
(
––+ ,
)
––, -
)
––- .
.
—— 
ToList
—— 
(
——  
)
——  !
;
——! "
if
”” 
(
”” 
urls
”” 
.
”” 
Count
”” "
>
””# $
$num
””% &
)
””& '
{
‘‘ 
_context
’’  
.
’’  !
Imagen
’’! '
.
’’' (
AddRange
’’( 0
(
’’0 1
urls
÷÷  
.
÷÷  !
Select
÷÷! '
(
÷÷' (
(
÷÷( )
url
÷÷) ,
,
÷÷, -
i
÷÷. /
)
÷÷/ 0
=>
÷÷1 3
new
÷÷4 7
Imagen
÷÷8 >
{
◊◊ 

IdInmueble
ÿÿ  *
=
ÿÿ+ ,
inmueble
ÿÿ- 5
.
ÿÿ5 6

IdInmueble
ÿÿ6 @
,
ÿÿ@ A
Url
ŸŸ  #
=
ŸŸ$ %
url
ŸŸ& )
,
ŸŸ) *
Portada
⁄⁄  '
=
⁄⁄( )
i
⁄⁄* +
==
⁄⁄, .
$num
⁄⁄/ 0
}
€€ 
)
€€ 
)
€€ 
;
€€  
}
‹‹ 
await
ﬁﬁ 
_context
ﬁﬁ "
.
ﬁﬁ" #
SaveChangesAsync
ﬁﬁ# 3
(
ﬁﬁ3 4
)
ﬁﬁ4 5
;
ﬁﬁ5 6
await
ﬂﬂ 
tx
ﬂﬂ 
.
ﬂﬂ 
CommitAsync
ﬂﬂ (
(
ﬂﬂ( )
)
ﬂﬂ) *
;
ﬂﬂ* +
return
‡‡ 
inmueble
‡‡ #
;
‡‡# $
}
·· 
catch
‚‚ 
{
„„ 
await
‰‰ 
tx
‰‰ 
.
‰‰ 
RollbackAsync
‰‰ *
(
‰‰* +
)
‰‰+ ,
;
‰‰, -
throw
ÂÂ 
;
ÂÂ 
}
ÊÊ 
}
ÁÁ 
)
ÁÁ 
;
ÁÁ 
}
ËË 	
}
ÈÈ 
}ÎÎ ¥!
sC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Repositories\InmuebleCaracteristicaRepository.cs
	namespace		 	
rever		
 
.		 
Repositories		 
{

 
public 

class ,
 InmuebleCaracteristicaRepository 1
:2 3-
!IInmuebleCaracteristicaRepository4 U
{ 
private 
readonly 
DatabaseService (
_context) 1
;1 2
public ,
 InmuebleCaracteristicaRepository /
(/ 0
DatabaseService0 ?
context@ G
)G H
{ 	
this 
. 
_context 
= 
context "
;" #
} 	
public 
async 
Task 
< 
List 
< "
InmuebleCaracteristica 5
>5 6
>6 7%
GetInmuebleCaracteristica8 Q
(Q R
)R S
{ 	
var 
data 
= 
await 
_context %
.% &"
InmuebleCaracteristica& <
.< =
ToListAsync= H
(H I
)I J
;J K
return 
data 
; 
} 	
public 
async 
Task 
< "
InmuebleCaracteristica 0
>0 1
GetByIds2 :
(: ;
int; >

idInmueble? I
,I J
intK N
idCaracteristicaO _
)_ `
{ 	
var 
data 
= 
await 
_context %
.% &"
InmuebleCaracteristica& <
. 
FirstOrDefaultAsync $
($ %
x% &
=>' )
x* +
.+ ,

IdInmueble, 6
==7 9

idInmueble: D
&&E G
xH I
.I J
IdCaracteristicaJ Z
==[ ]
idCaracteristica^ n
)n o
;o p
return 
data 
; 
} 	
public!! 
async!! 
Task!! 
<!! 
bool!! 
>!! &
PostInmuebleCaracteristica!!  :
(!!: ;"
InmuebleCaracteristica!!; Q"
inmuebleCaracteristica!!R h
)!!h i
{"" 	
var## 
exist## 
=## 
await## 
_context## &
.##& '"
InmuebleCaracteristica##' =
.$$ 
FirstOrDefaultAsync$$ $
($$$ %
x$$% &
=>$$' )
x$$* +
.$$+ ,

IdInmueble$$, 6
==$$7 9"
inmuebleCaracteristica$$: P
.$$P Q

IdInmueble$$Q [
&&%%( *
x%%+ ,
.%%, -
IdCaracteristica%%- =
==%%> @"
inmuebleCaracteristica%%A W
.%%W X
IdCaracteristica%%X h
)%%h i
;%%i j
if'' 
('' 
exist'' 
!='' 
null'' 
)'' 
{(( 
return)) 
false)) 
;)) 
}** 
await,, 
_context,, 
.,, "
InmuebleCaracteristica,, 1
.,,1 2
AddAsync,,2 :
(,,: ;"
inmuebleCaracteristica,,; Q
),,Q R
;,,R S
await-- 
_context-- 
.-- 
SaveChangesAsync-- +
(--+ ,
)--, -
;--- .
return.. 
true.. 
;.. 
}// 	
public11 
async11 
Task11 
<11 
bool11 
>11 (
DeleteInmuebleCaracteristica11  <
(11< ="
InmuebleCaracteristica11= S"
inmuebleCaracteristica11T j
)11j k
{22 	
_context33 
.33 "
InmuebleCaracteristica33 +
.33+ ,
Remove33, 2
(332 3"
inmuebleCaracteristica333 I
)33I J
;33J K
await44 
_context44 
.44 
SaveChangesAsync44 +
(44+ ,
)44, -
;44- .
return55 
true55 
;55 
}66 	
}77 
}88 ß"
cC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Repositories\ImagenRepository.cs
	namespace 	
rever
 
. 
Repositories 
{		 
public

 

class

 
ImagenRepository

 !
:

" #
IImagenRepository

$ 5
{ 
private 
readonly 
DatabaseService (
_context) 1
;1 2
public 
ImagenRepository 
(  
DatabaseService  /
context0 7
)7 8
{ 	
this 
. 
_context 
= 
context "
;" #
} 	
public 
async 
Task 
< 
List 
< 
Imagen %
>% &
>& '
	GetImagen( 1
(1 2
)2 3
{ 	
var 
data 
= 
await 
_context %
.% &
Imagen& ,
., -
ToListAsync- 8
(8 9
)9 :
;: ;
return 
data 
; 
} 	
public 
async 
Task 
< 
Imagen  
>  !
GetImagenById" /
(/ 0
int0 3
id4 6
)6 7
{ 	
var 
data 
= 
await 
_context %
.% &
Imagen& ,
., -
FirstOrDefaultAsync- @
(@ A
xA B
=>C E
xF G
.G H
IdImagenH P
==Q S
idT V
)V W
;W X
return 
data 
; 
} 	
public 
async 
Task 
< 
bool 
> 

PostImagen  *
(* +
Imagen+ 1
imagen2 8
)8 9
{   	
await!! 
_context!! 
.!! 
Imagen!! !
.!!! "
AddAsync!!" *
(!!* +
imagen!!+ 1
)!!1 2
;!!2 3
await"" 
_context"" 
."" 
SaveChangesAsync"" +
(""+ ,
)"", -
;""- .
return## 
true## 
;## 
}$$ 	
public&& 
async&& 
Task&& 
<&& 
bool&& 
>&& 
	PutImagen&&  )
(&&) *
Imagen&&* 0
imagen&&1 7
)&&7 8
{'' 	
var(( 
exist(( 
=(( 
_context((  
.((  !
Imagen((! '
.((' (
FirstOrDefault((( 6
(((6 7
x((7 8
=>((9 ;
x((< =
.((= >
IdImagen((> F
==((G I
imagen((J P
.((P Q
IdImagen((Q Y
)((Y Z
;((Z [
if)) 
()) 
exist)) 
==)) 
null)) 
))) 
{** 
return++ 
false++ 
;++ 
},, 
exist.. 
... 
Url.. 
=.. 
imagen.. 
... 
Url.. "
;.." #
exist// 
.// 

IdInmueble// 
=// 
imagen// %
.//% &

IdInmueble//& 0
;//0 1
_context00 
.00 
Imagen00 
.00 
Update00 "
(00" #
imagen00# )
)00) *
;00* +
await11 
_context11 
.11 
SaveChangesAsync11 +
(11+ ,
)11, -
;11- .
return22 
true22 
;22 
}33 	
public55 
async55 
Task55 
<55 
bool55 
>55 
DeleteImagen55  ,
(55, -
Imagen55- 3
imagen554 :
)55: ;
{66 	
_context77 
.77 
Imagen77 
.77 
Remove77 "
(77" #
imagen77# )
)77) *
;77* +
await88 
_context88 
.88 
SaveChangesAsync88 +
(88+ ,
)88, -
;88- .
return99 
true99 
;99 
}:: 	
};; 
}<< Œ#
nC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Repositories\EstadoPublicacionRepository.cs
	namespace 	
rever
 
. 
Repositories 
{		 
public

 

class

 '
EstadoPublicacionRepository

 ,
:

- .(
IEstadoPublicacionRepository

/ K
{ 
private 
readonly 
DatabaseService (
_context) 1
;1 2
public '
EstadoPublicacionRepository *
(* +
DatabaseService+ :
context; B
)B C
{ 	
this 
. 
_context 
= 
context "
;" #
} 	
public 
async 
Task 
< 
List 
< 
EstadoPublicacion 0
>0 1
>1 2 
GetEstadoPublicacion3 G
(G H
)H I
{ 	
var 
data 
= 
await 
_context %
.% &
EstadoPublicacion& 7
.7 8
ToListAsync8 C
(C D
)D E
;E F
return 
data 
; 
} 	
public 
async 
Task 
< 
EstadoPublicacion +
>+ ,$
GetEstadoPublicacionById- E
(E F
intF I
idJ L
)L M
{ 	
var 
data 
= 
await 
_context %
.% &
EstadoPublicacion& 7
.7 8
FirstOrDefaultAsync8 K
(K L
xL M
=>N P
xQ R
.R S
IdEstadoS [
==\ ^
id_ a
)a b
;b c
return 
data 
; 
} 	
public 
async 
Task 
< 
bool 
> !
PostEstadoPublicacion  5
(5 6
EstadoPublicacion6 G
estadoPublicacionH Y
)Y Z
{   	
await!! 
_context!! 
.!! 
EstadoPublicacion!! ,
.!!, -
AddAsync!!- 5
(!!5 6
estadoPublicacion!!6 G
)!!G H
;!!H I
await"" 
_context"" 
."" 
SaveChangesAsync"" +
(""+ ,
)"", -
;""- .
return## 
true## 
;## 
}$$ 	
public&& 
async&& 
Task&& 
<&& 
bool&& 
>&&  
PutEstadoPublicacion&&  4
(&&4 5
EstadoPublicacion&&5 F
estadoPublicacion&&G X
)&&X Y
{'' 	
var(( 
exist(( 
=(( 
_context((  
.((  !
EstadoPublicacion((! 2
.((2 3
FirstOrDefault((3 A
(((A B
x((B C
=>((D F
x((G H
.((H I
IdEstado((I Q
==((R T
estadoPublicacion((U f
.((f g
IdEstado((g o
)((o p
;((p q
if)) 
()) 
exist)) 
==)) 
null)) 
))) 
{** 
return++ 
false++ 
;++ 
},, 
exist.. 
... 
Nombre.. 
=.. 
estadoPublicacion.. ,
..., -
Nombre..- 3
;..3 4
_context// 
.// 
EstadoPublicacion// &
.//& '
Update//' -
(//- .
estadoPublicacion//. ?
)//? @
;//@ A
await00 
_context00 
.00 
SaveChangesAsync00 +
(00+ ,
)00, -
;00- .
return11 
true11 
;11 
}22 	
public44 
async44 
Task44 
<44 
bool44 
>44 #
DeleteEstadoPublicacion44  7
(447 8
EstadoPublicacion448 I
estadoPublicacion44J [
)44[ \
{55 	
_context66 
.66 
EstadoPublicacion66 &
.66& '
Remove66' -
(66- .
estadoPublicacion66. ?
)66? @
;66@ A
await77 
_context77 
.77 
SaveChangesAsync77 +
(77+ ,
)77, -
;77- .
return88 
true88 
;88 
}99 	
}:: 
};; ¶&
eC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Repositories\ContactoRepository.cs
	namespace 	
rever
 
. 
Repositories 
{		 
public

 

class

 
ContactoRepository

 #
:

$ %
IContactoRepository

& 9
{ 
private 
readonly 
DatabaseService (
_context) 1
;1 2
public 
ContactoRepository !
(! "
DatabaseService" 1
context2 9
)9 :
{ 	
this 
. 
_context 
= 
context "
;" #
} 	
public 
async 
Task 
< 
List 
< 
Contacto '
>' (
>( )
GetContacto* 5
(5 6
)6 7
{ 	
var 
data 
= 
await 
_context %
.% &
Contacto& .
.. /
ToListAsync/ :
(: ;
); <
;< =
return 
data 
; 
} 	
public 
async 
Task 
< 
Contacto "
>" #
GetContactoById$ 3
(3 4
int4 7
id8 :
): ;
{ 	
var 
data 
= 
await 
_context %
.% &
Contacto& .
.. /
FirstOrDefaultAsync/ B
(B C
xC D
=>E G
xH I
.I J

IdContactoJ T
==U W
idX Z
)Z [
;[ \
return 
data 
; 
} 	
public 
async 
Task 
< 
bool 
> 
PostContacto  ,
(, -
Contacto- 5
contacto6 >
)> ?
{   	
await!! 
_context!! 
.!! 
Contacto!! #
.!!# $
AddAsync!!$ ,
(!!, -
contacto!!- 5
)!!5 6
;!!6 7
await"" 
_context"" 
."" 
SaveChangesAsync"" +
(""+ ,
)"", -
;""- .
return## 
true## 
;## 
}$$ 	
public&& 
async&& 
Task&& 
<&& 
bool&& 
>&& 
PutContacto&&  +
(&&+ ,
Contacto&&, 4
contacto&&5 =
)&&= >
{'' 	
var(( 
exist(( 
=(( 
_context((  
.((  !
Contacto((! )
.(() *
FirstOrDefault((* 8
(((8 9
x((9 :
=>((; =
x((> ?
.((? @

IdContacto((@ J
==((K M
contacto((N V
.((V W

IdContacto((W a
)((a b
;((b c
if)) 
()) 
exist)) 
==)) 
null)) 
))) 
{** 
return++ 
false++ 
;++ 
},, 
exist.. 
... 
IdComprador.. 
=.. 
contacto..  (
...( )
IdComprador..) 4
;..4 5
exist// 
.// 

IdVendedor// 
=// 
contacto// '
.//' (

IdVendedor//( 2
;//2 3
exist00 
.00 

IdInmueble00 
=00 
contacto00 '
.00' (

IdInmueble00( 2
;002 3
exist11 
.11 
Mensaje11 
=11 
contacto11 $
.11$ %
Mensaje11% ,
;11, -
exist22 
.22 
Fecha22 
=22 
contacto22 "
.22" #
Fecha22# (
;22( )
_context33 
.33 
Contacto33 
.33 
Update33 $
(33$ %
contacto33% -
)33- .
;33. /
await44 
_context44 
.44 
SaveChangesAsync44 +
(44+ ,
)44, -
;44- .
return55 
true55 
;55 
}66 	
public88 
async88 
Task88 
<88 
bool88 
>88 
DeleteContacto88  .
(88. /
Contacto88/ 7
contacto888 @
)88@ A
{99 	
_context:: 
.:: 
Contacto:: 
.:: 
Remove:: $
(::$ %
contacto::% -
)::- .
;::. /
await;; 
_context;; 
.;; 
SaveChangesAsync;; +
(;;+ ,
);;, -
;;;- .
return<< 
true<< 
;<< 
}== 	
}>> 
}?? ö!
cC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Repositories\CiudadRepository.cs
	namespace		 	
rever		
 
.		 
Repositories		 
{

 
public 

class 
CiudadRepository !
:" #
ICiudadRepository$ 5
{ 
private 
readonly 
DatabaseService (
_context) 1
;1 2
public 
CiudadRepository 
(  
DatabaseService  /
context0 7
)7 8
{ 	
this 
. 
_context 
= 
context "
;" #
} 	
public 
async 
Task 
< 
List 
< 
Ciudad %
>% &
>& '
	GetCiudad( 1
(1 2
)2 3
{ 	
var 
data 
= 
await 
_context %
.% &
Ciudad& ,
., -
ToListAsync- 8
(8 9
)9 :
;: ;
return 
data 
; 
} 	
public 
async 
Task 
< 
Ciudad  
>  !
GetCiudadById" /
(/ 0
int0 3
id4 6
)6 7
{ 	
var 
data 
= 
await 
_context %
.% &
Ciudad& ,
., -
FirstOrDefaultAsync- @
(@ A
xA B
=>C E
xF G
.G H
IdCiudadH P
==Q S
idT V
)V W
;W X
return 
data 
; 
} 	
public   
async   
Task   
<   
bool   
>   

PostCiudad    *
(  * +
Ciudad  + 1
ciudad  2 8
)  8 9
{!! 	
await"" 
_context"" 
."" 
Ciudad"" !
.""! "
AddAsync""" *
(""* +
ciudad""+ 1
)""1 2
;""2 3
await## 
_context## 
.## 
SaveChangesAsync## +
(##+ ,
)##, -
;##- .
return$$ 
true$$ 
;$$ 
}%% 	
public'' 
async'' 
Task'' 
<'' 
bool'' 
>'' 
	PutCiudad''  )
('') *
Ciudad''* 0
ciudad''1 7
)''7 8
{(( 	
var)) 
exist)) 
=)) 
_context))  
.))  !
Ciudad))! '
.))' (
FirstOrDefault))( 6
())6 7
x))7 8
=>))9 ;
x))< =
.))= >
IdCiudad))> F
==))G I
ciudad))J P
.))P Q
IdCiudad))Q Y
)))Y Z
;))Z [
if** 
(** 
exist** 
==** 
null** 
)** 
{++ 
return,, 
false,, 
;,, 
}-- 
exist// 
.// 
Nombre// 
=// 
ciudad// !
.//! "
Nombre//" (
;//( )
_context00 
.00 
Ciudad00 
.00 
Update00 "
(00" #
ciudad00# )
)00) *
;00* +
await11 
_context11 
.11 
SaveChangesAsync11 +
(11+ ,
)11, -
;11- .
return22 
true22 
;22 
}33 	
public55 
async55 
Task55 
<55 
bool55 
>55 
DeleteCiudad55  ,
(55, -
Ciudad55- 3
ciudad554 :
)55: ;
{66 	
_context77 
.77 
Ciudad77 
.77 
Remove77 "
(77" #
ciudad77# )
)77) *
;77* +
await88 
_context88 
.88 
SaveChangesAsync88 +
(88+ ,
)88, -
;88- .
return99 
true99 
;99 
}:: 	
};; 
}<< í#
kC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Repositories\CaracteristicaRepository.cs
	namespace		 	
rever		
 
.		 
Repositories		 
{

 
public 

class $
CaracteristicaRepository )
:* +%
ICaracteristicaRepository, E
{ 
private 
readonly 
DatabaseService (
_context) 1
;1 2
public $
CaracteristicaRepository '
(' (
DatabaseService( 7
context8 ?
)? @
{ 	
this 
. 
_context 
= 
context "
;" #
} 	
public 
async 
Task 
< 
List 
< 
Caracteristica -
>- .
>. /
GetCaracteristica0 A
(A B
)B C
{ 	
var 
data 
= 
await 
_context %
.% &
Caracteristica& 4
.4 5
ToListAsync5 @
(@ A
)A B
;B C
return 
data 
; 
} 	
public 
async 
Task 
< 
Caracteristica (
>( )!
GetCaracteristicaById* ?
(? @
int@ C
idD F
)F G
{ 	
var 
data 
= 
await 
_context %
.% &
Caracteristica& 4
.4 5
FirstOrDefaultAsync5 H
(H I
xI J
=>K M
xN O
.O P
IdCaracteristicaP `
==a c
idd f
)f g
;g h
return 
data 
; 
} 	
public   
async   
Task   
<   
bool   
>   
PostCaracteristica    2
(  2 3
Caracteristica  3 A
caracteristica  B P
)  P Q
{!! 	
await"" 
_context"" 
."" 
Caracteristica"" )
."") *
AddAsync""* 2
(""2 3
caracteristica""3 A
)""A B
;""B C
await## 
_context## 
.## 
SaveChangesAsync## +
(##+ ,
)##, -
;##- .
return$$ 
true$$ 
;$$ 
}%% 	
public'' 
async'' 
Task'' 
<'' 
bool'' 
>'' 
PutCaracteristica''  1
(''1 2
Caracteristica''2 @
caracteristica''A O
)''O P
{(( 	
var)) 
exist)) 
=)) 
_context))  
.))  !
Caracteristica))! /
.))/ 0
FirstOrDefault))0 >
())> ?
x))? @
=>))A C
x))D E
.))E F
IdCaracteristica))F V
==))W Y
caracteristica))Z h
.))h i
IdCaracteristica))i y
)))y z
;))z {
if** 
(** 
exist** 
==** 
null** 
)** 
{++ 
return,, 
false,, 
;,, 
}-- 
exist// 
.// 
Nombre// 
=// 
caracteristica// )
.//) *
Nombre//* 0
;//0 1
_context00 
.00 
Caracteristica00 #
.00# $
Update00$ *
(00* +
caracteristica00+ 9
)009 :
;00: ;
await11 
_context11 
.11 
SaveChangesAsync11 +
(11+ ,
)11, -
;11- .
return22 
true22 
;22 
}33 	
public55 
async55 
Task55 
<55 
bool55 
>55  
DeleteCaracteristica55  4
(554 5
Caracteristica555 C
caracteristica55D R
)55R S
{66 	
_context77 
.77 
Caracteristica77 #
.77# $
Remove77$ *
(77* +
caracteristica77+ 9
)779 :
;77: ;
await88 
_context88 
.88 
SaveChangesAsync88 +
(88+ ,
)88, -
;88- .
return99 
true99 
;99 
}:: 	
};; 
}<< ‹6
MC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Program.cs
var 
builder 
= 
WebApplication 
. 
CreateBuilder *
(* +
args+ /
)/ 0
;0 1
builder		 
.		 
Services		 
.		 
AddControllers		 
(		  
)		  !
;		! "
builder

 
.

 
Services

 
.

 #
AddEndpointsApiExplorer

 (
(

( )
)

) *
;

* +
builder 
. 
Services 
. 
AddSwaggerGen 
( 
options &
=>' )
{ 
options 
. 

SwaggerDoc 
( 
$str 
, 
new  
OpenApiInfo! ,
{- .
Title/ 4
=5 6
$str7 B
,B C
VersionD K
=L M
$strN R
}S T
)T U
;U V
options 
. !
AddSecurityDefinition !
(! "
$str" *
,* +
new, /!
OpenApiSecurityScheme0 E
{ 
Name 
= 
$str 
, 
Type 
= 
SecuritySchemeType !
.! "
Http" &
,& '
Scheme 
= 
$str 
, 
BearerFormat 
= 
$str 
, 
In 

= 
ParameterLocation 
. 
Header %
,% &
Description 
= 
$str N
} 
) 
; 
options 
. "
AddSecurityRequirement "
(" #
new# &&
OpenApiSecurityRequirement' A
{ 
{ 	
new !
OpenApiSecurityScheme %
{ 
	Reference 
= 
new 
OpenApiReference  0
{   
Type!! 
=!! 
ReferenceType!! (
.!!( )
SecurityScheme!!) 7
,!!7 8
Id"" 
="" 
$str"" !
}## 
}$$ 
,$$ 
Array%% 
.%% 
Empty%% 
<%% 
string%% 
>%% 
(%%  
)%%  !
}&& 	
}'' 
)'' 
;'' 
}(( 
)(( 
;(( 
builder++ 
.++ 
Services++ 
.++ 
AddExternal++ 
(++ 
builder++ $
.++$ %
Configuration++% 2
)++2 3
;++3 4
var.. 
jwtSettings.. 
=.. 
builder.. 
... 
Configuration.. '
...' (

GetSection..( 2
(..2 3
$str..3 8
)..8 9
;..9 :
var// 
key// 
=// 	
Encoding//
 
.// 
UTF8// 
.// 
GetBytes//  
(//  !
jwtSettings//! ,
[//, -
$str//- 2
]//2 3
!//3 4
)//4 5
;//5 6
builder11 
.11 
Services11 
.11 
AddAuthentication11 "
(11" #
JwtBearerDefaults11# 4
.114 5 
AuthenticationScheme115 I
)11I J
.22 
AddJwtBearer22 
(22 
options22 
=>22 
{33 
options44 
.44 %
TokenValidationParameters44 )
=44* +
new44, /%
TokenValidationParameters440 I
{55 	
ValidateIssuer66 
=66 
true66 !
,66! "
ValidateAudience77 
=77 
true77 #
,77# $
ValidateLifetime88 
=88 
true88 #
,88# $$
ValidateIssuerSigningKey99 $
=99% &
true99' +
,99+ ,
ValidIssuer:: 
=:: 
jwtSettings:: %
[::% &
$str::& .
]::. /
,::/ 0
ValidAudience;; 
=;; 
jwtSettings;; '
[;;' (
$str;;( 2
];;2 3
,;;3 4
IssuerSigningKey<< 
=<< 
new<< " 
SymmetricSecurityKey<<# 7
(<<7 8
key<<8 ;
)<<; <
}== 	
;==	 

}>> 
)>> 
;>> 
builder@@ 
.@@ 
Services@@ 
.@@ 
AddAuthorization@@ !
(@@! "
)@@" #
;@@# $
varBB 
allowedOriginsBB 
=BB 
builderBB 
.BB 
ConfigurationBB *
.BB* +

GetSectionBB+ 5
(BB5 6
$strBB6 K
)BBK L
.BBL M
GetBBM P
<BBP Q
stringBBQ W
[BBW X
]BBX Y
>BBY Z
(BBZ [
)BB[ \
??CC 
[CC 
$strCC 0
,CC0 1
$strCC2 I
]CCI J
;CCJ K
builderDD 
.DD 
ServicesDD 
.DD 
AddCorsDD 
(DD 
optionsDD  
=>DD! #
{EE 
optionsFF 
.FF 
	AddPolicyFF 
(FF 
$strFF (
,FF( )
policyFF* 0
=>FF1 3
{GG 
policyHH 
.HH 
WithOriginsHH 
(HH 
allowedOriginsHH )
)HH) *
.II 
AllowAnyHeaderII 
(II 
)II 
.JJ 
AllowAnyMethodJJ 
(JJ 
)JJ 
;JJ  
}KK 
)KK 
;KK 
}LL 
)LL 
;LL 
varNN 
appNN 
=NN 	
builderNN
 
.NN 
BuildNN 
(NN 
)NN 
;NN 
appPP 
.PP 
UseCorsPP 
(PP 
$strPP 
)PP 
;PP  
ifRR 
(RR 
appRR 
.RR 
EnvironmentRR 
.RR 
IsDevelopmentRR !
(RR! "
)RR" #
)RR# $
{SS 
appTT 
.TT 

UseSwaggerTT 
(TT 
)TT 
;TT 
appUU 
.UU 
UseSwaggerUIUU 
(UU 
)UU 
;UU 
}VV 
appXX 
.XX 
UseHttpsRedirectionXX 
(XX 
)XX 
;XX 
appZZ 
.ZZ 
UseAuthenticationZZ 
(ZZ 
)ZZ 
;ZZ 
app[[ 
.[[ 
UseAuthorization[[ 
([[ 
)[[ 
;[[ 
app]] 
.]] 
MapControllers]] 
(]] 
)]] 
;]] 
app__ 
.__ 
Run__ 
(__ 
)__ 	
;__	 
≈'
TC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Models\Usuario.cs
	namespace 	
rever
 
. 
Models 
{ 
[ 
Table 

(
 
$str 
) 
] 
public 

class 
Usuario 
{		 
[

 	
Key

	 
]

 
[ 	
Column	 
( 
$str 
) 
] 
public 
int 
	IdUsuario 
{ 
get "
;" #
set$ '
;' (
}) *
[ 	
Required	 
( 
ErrorMessage 
=  
$str! <
)< =
]= >
[ 	
StringLength	 
( 
$num 
) 
] 
[ 	
Column	 
( 
$str 
) 
] 
public 
required 
string 
Nombre %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
[ 	
Required	 
( 
ErrorMessage 
=  
$str! <
)< =
]= >
[ 	
EmailAddress	 
( 
ErrorMessage "
=# $
$str% J
)J K
]K L
[ 	
StringLength	 
( 
$num 
) 
] 
[ 	
Column	 
( 
$str 
) 
] 
public 
required 
string 
Correo %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
[ 	
Required	 
( 
ErrorMessage 
=  
$str! @
)@ A
]A B
[ 	
StringLength	 
( 
$num 
, 
MinimumLength (
=) *
$num+ ,
), -
]- .
[ 	
DataType	 
( 
DataType 
. 
Password #
)# $
]$ %
[ 	
Column	 
( 
$str 
) 
] 
public 
required 
string 
Contrase√±a )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
[ 	
Required	 
( 
ErrorMessage 
=  !
$str" ?
)? @
]@ A
[   	
Phone  	 
(   
ErrorMessage   
=   
$str   E
)  E F
]  F G
[!! 	
StringLength!!	 
(!! 
$num!! 
)!! 
]!! 
["" 	
Column""	 
("" 
$str"" 
)"" 
]"" 
public## 
required## 
string## 
Telefono## '
{##( )
get##* -
;##- .
set##/ 2
;##2 3
}##4 5
[%% 	
Required%%	 
(%% 
ErrorMessage%% 
=%%  
$str%%! @
)%%@ A
]%%A B
[&& 	
Range&&	 
(&& 
$num&& 
,&& 
$num&& 
,&& 
ErrorMessage&& !
=&&" #
$str&&$ K
)&&K L
]&&L M
['' 	
Column''	 
('' 
$str'' 
)'' 
]'' 
public(( 
required(( 
int(( 
IdRol(( !
{((" #
get(($ '
;((' (
set(() ,
;((, -
}((. /
[** 	
Column**	 
(** 
$str**  
)**  !
]**! "
public++ 
DateTime++ 
FechaRegistro++ %
{++& '
get++( +
;+++ ,
set++- 0
;++0 1
}++2 3
=++4 5
DateTime++6 >
.++> ?
Now++? B
;++B C
[-- 	
Column--	 
(-- 
$str-- 
)-- 
]-- 
public.. 
bool.. 

Confirmado.. 
{..  
get..! $
;..$ %
set..& )
;..) *
}..+ ,
=..- .
false../ 4
;..4 5
[00 	
Column00	 
(00 
$str00 $
)00$ %
]00% &
public11 
string11 
?11 
TokenConfirmacion11 (
{11) *
get11+ .
;11. /
set110 3
;113 4
}115 6
[33 	

ForeignKey33	 
(33 
$str33 
)33 
]33 
public44 
Rol44 
?44 
Rol44 
{44 
get44 
;44 
set44 "
;44" #
}44$ %
}55 
}66 Ø	
YC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Models\TipoInmueble.cs
	namespace 	
rever
 
. 
Models 
{ 
[ 
Table 

(
 
$str 
) 
] 
public 

class 
TipoInmueble 
{ 
[		 	
Key			 
]		 
[

 	
Column

	 
(

 
$str

 
)

 
]

 
public 
int 
IdTipo 
{ 
get 
;  
set! $
;$ %
}& '
[ 	
Required	 
( 
ErrorMessage 
=  
$str! <
)< =
]= >
[ 	
StringLength	 
( 
$num 
) 
] 
[ 	
Column	 
( 
$str 
) 
] 
public 
required 
string 
Nombre %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
} 
} ú	
PC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Models\Rol.cs
	namespace 	
rever
 
. 
Models 
{ 
[ 
Table 

(
 
$str 
) 
] 
public 

class 
Rol 
{ 
[		 	
Key			 
]		 
[

 	
Column

	 
(

 
$str

 
)

 
]

 
public 
int 
IdRol 
{ 
get 
; 
set  #
;# $
}% &
[ 	
Required	 
( 
ErrorMessage 
=  
$str! <
)< =
]= >
[ 	
StringLength	 
( 
$num 
) 
] 
[ 	
Column	 
( 
$str 
) 
] 
public 
required 
string 
Nombre %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
} 
} Ú
\C:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Models\ModoTransaccion.cs
	namespace 	
rever
 
. 
Models 
{ 
public 

class 
ModoTransaccion  
{ 
[ 	
Key	 
] 
[		 	
Column			 
(		 
$str		 
)		 
]		 
public

 
int

 
IdModo

 
{

 
get

 
;

  
set

! $
;

$ %
}

& '
[ 	
Required	 
( 
ErrorMessage 
=  
$str! <
)< =
]= >
[ 	
StringLength	 
( 
$num 
) 
] 
public 
required 
string 
Nombre %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
} 
} ê
VC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Models\Localidad.cs
	namespace 	
rever
 
. 
Models 
{ 
[ 
Table 

(
 
$str 
) 
] 
public 

class 
	Localidad 
{ 
[		 	
Key			 
]		 
[

 	
Column

	 
(

 
$str

 
)

 
]

  
public 
int 
IdLocalidad 
{  
get! $
;$ %
set& )
;) *
}+ ,
[ 	
Required	 
( 
ErrorMessage 
=  
$str! <
)< =
]= >
[ 	
StringLength	 
( 
$num 
, 
MinimumLength (
=) *
$num+ ,
), -
]- .
[ 	
Column	 
( 
$str 
) 
] 
public 
required 
string 
Nombre %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
[ 	
Required	 
( 
ErrorMessage 
=  
$str! E
)E F
]F G
[ 	
Column	 
( 
$str 
) 
] 
public 
int 
IdCiudad 
{ 
get !
;! "
set# &
;& '
}( )
[ 	

ForeignKey	 
( 
$str 
) 
]  
public 
Ciudad 
? 
Ciudad 
{ 
get  #
;# $
set% (
;( )
}* +
} 
} –	
RC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Models\Login.cs
	namespace 	
rever
 
. 
Models 
{ 
public 

class 
Login 
{ 
[ 	
Required	 
( 
ErrorMessage 
=  
$str! ;
); <
]< =
[ 	
EmailAddress	 
( 
ErrorMessage "
=# $
$str% J
)J K
]K L
public		 
required		 
string		 
Correo		 %
{		& '
get		( +
;		+ ,
set		- 0
;		0 1
}		2 3
[ 	
Required	 
( 
ErrorMessage 
=  
$str! ?
)? @
]@ A
[ 	
	MinLength	 
( 
$num 
, 
ErrorMessage "
=# $
$str% U
)U V
]V W
public 
required 
string 
Contrase√±a )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
} 
} Ê
cC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Models\InmuebleCaracteristica.cs
	namespace 	
rever
 
. 
Models 
{ 
[ 
Table 

(
 
$str $
)$ %
]% &
public 

class "
InmuebleCaracteristica '
{ 
[		 	
Required			 
(		 
ErrorMessage		 
=		  
$str		! E
)		E F
]		F G
[

 	
Column

	 
(

 
$str

 
,

 
Order

 $
=

% &
$num

' (
)

( )
]

) *
public 
int 

IdInmueble 
{ 
get  #
;# $
set% (
;( )
}* +
[ 	
Required	 
( 
ErrorMessage 
=  
$str! M
)M N
]N O
[ 	
Column	 
( 
$str #
,# $
Order% *
=+ ,
$num- .
). /
]/ 0
public 
int 
IdCaracteristica #
{$ %
get& )
;) *
set+ .
;. /
}0 1
[ 	

ForeignKey	 
( 
$str  
)  !
]! "
public 
Inmueble 
? 
Inmueble !
{" #
get$ '
;' (
set) ,
;, -
}. /
[ 	

ForeignKey	 
( 
$str &
)& '
]' (
public 
Caracteristica 
? 
Caracteristica -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
} 
} ª	
^C:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Models\EstadoPublicacion.cs
	namespace 	
rever
 
. 
Models 
{ 
[ 
Table 

(
 
$str  
)  !
]! "
public 

class 
EstadoPublicacion "
{		 
[

 	
Key

	 
]

 
[ 	
Column	 
( 
$str 
) 
] 
public 
int 
IdEstado 
{ 
get !
;! "
set# &
;& '
}( )
[ 	
Required	 
( 
ErrorMessage 
=  
$str! <
)< =
]= >
[ 	
StringLength	 
( 
$num 
) 
] 
[ 	
Column	 
( 
$str 
) 
] 
public 
required 
string 
Nombre %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
} 
} –
SC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Models\Imagen.cs
	namespace 	
rever
 
. 
Models 
{ 
[ 
Table 

(
 
$str 
) 
] 
public 

class 
Imagen 
{		 
[

 	
Key

	 
]

 
[ 	
Column	 
( 
$str 
) 
] 
public 
int 
IdImagen 
{ 
get !
;! "
set# &
;& '
}( )
[ 	
Required	 
( 
ErrorMessage 
=  
$str! 9
)9 :
]: ;
[ 	
StringLength	 
( 
$num 
) 
] 
[ 	
Column	 
( 
$str 
) 
] 
public 
required 
string 
Url "
{# $
get% (
;( )
set* -
;- .
}/ 0
[ 	
Column	 
( 
$str 
) 
] 
public 
bool 
Portada 
{ 
get !
;! "
set# &
;& '
}( )
[ 	
Required	 
( 
ErrorMessage 
=  
$str! E
)E F
]F G
[ 	
Column	 
( 
$str 
) 
] 
public 
required 
int 

IdInmueble &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
[ 	

ForeignKey	 
( 
$str  
)  !
]! "
public 
Inmueble 
? 
Inmueble !
{" #
get$ '
;' (
set) ,
;, -
}. /
} 
} ¶O
UC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Models\Inmueble.cs
	namespace 	
rever
 
. 
Models 
{ 
[ 
Table 

(
 
$str 
) 
] 
public 

class 
Inmueble 
{		 
[

 	
Key

	 
]

 
[ 	
Column	 
( 
$str 
) 
] 
public 
int 

IdInmueble 
{ 
get  #
;# $
set% (
;( )
}* +
[ 	
Required	 
( 
ErrorMessage 
=  
$str! <
)< =
]= >
[ 	
StringLength	 
( 
$num 
) 
] 
[ 	
Column	 
( 
$str 
) 
] 
public 
required 
string 
Titulo %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
[ 	
Required	 
( 
ErrorMessage 
=  
$str! A
)A B
]B C
[ 	
Column	 
( 
$str 
, 
TypeName '
=( )
$str* 0
)0 1
]1 2
public 
required 
string 
Descripcion *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
[ 	
Column	 
( 
$str 
, 
TypeName "
=# $
$str% 4
)4 5
]5 6
public 
decimal 
Precio 
{ 
get  #
;# $
set% (
;( )
}* +
[ 	
Column	 
( 
$str 
) 
] 
public 
int 
IdTipo 
{ 
get 
;  
set! $
;$ %
}& '
[ 	
Column	 
( 
$str 
) 
] 
public 
int 
IdModo 
{ 
get 
;  
set! $
;$ %
}& '
[   	
Required  	 
(   
ErrorMessage   
=    
$str  ! ?
)  ? @
]  @ A
[!! 	
StringLength!!	 
(!! 
$num!! 
)!! 
]!! 
["" 	
Column""	 
("" 
$str"" 
)"" 
]"" 
public## 
required## 
string## 
	Direccion## (
{##) *
get##+ .
;##. /
set##0 3
;##3 4
}##5 6
[&& 	
Required&&	 
(&& 
ErrorMessage&& 
=&&  
$str&&! H
)&&H I
]&&I J
['' 	
Column''	 
('' 
$str'' 
)'' 
]''  
public(( 
required(( 
int(( 
IdLocalidad(( '
{((( )
get((* -
;((- .
set((/ 2
;((2 3
}((4 5
[** 	
Required**	 
(** 
ErrorMessage** 
=**  
$str**! L
)**L M
]**M N
[++ 	
Range++	 
(++ 
$num++ 
,++ 
$num++ 
)++ 
]++ 
[,, 	
Column,,	 
(,, 
$str,, 
),, 
],,  
public-- 
required-- 
int-- 
Habitaciones-- (
{--) *
get--+ .
;--. /
set--0 3
;--3 4
}--5 6
[// 	
Required//	 
(// 
ErrorMessage// 
=//  
$str//! E
)//E F
]//F G
[00 	
Range00	 
(00 
$num00 
,00 
$num00 
)00 
]00 
[11 	
Column11	 
(11 
$str11 
)11 
]11 
public22 
required22 
int22 
Ba√±os22 !
{22" #
get22$ '
;22' (
set22) ,
;22, -
}22. /
[44 	
Required44	 
(44 
ErrorMessage44 
=44  
$str44! P
)44P Q
]44Q R
[55 	
Column55	 
(55 
$str55 "
)55" #
]55# $
public66 
required66 
int66 
MetrosCuadrados66 +
{66, -
get66. 1
;661 2
set663 6
;666 7
}668 9
[88 	
Required88	 
(88 
ErrorMessage88 
=88  
$str88! G
)88G H
]88H I
[99 	
Range99	 
(99 
$num99 
,99 
$num99 
)99 
]99 
[:: 	
Column::	 
(:: 
$str:: 
):: 
]:: 
public;; 
int;; 
Estrato;; 
{;; 
get;;  
;;;  !
set;;" %
;;;% &
};;' (
[== 	
Column==	 
(== 
$str== 
)== 
]== 
public>> 
int>> 
Pisos>> 
{>> 
get>> 
;>> 
set>>  #
;>># $
}>>% &
[@@ 	
Required@@	 
(@@ 
ErrorMessage@@ 
=@@  
$str@@! =
)@@= >
]@@> ?
[AA 	
ColumnAA	 
(AA 
$strAA 
,AA 
TypeNameAA #
=AA$ %
$strAA& 5
)AA5 6
]AA6 7
publicBB 
requiredBB 
decimalBB 
LatitudBB  '
{BB( )
getBB* -
;BB- .
setBB/ 2
;BB2 3
}BB4 5
[DD 	
RequiredDD	 
(DD 
ErrorMessageDD 
=DD  
$strDD! >
)DD> ?
]DD? @
[EE 	
ColumnEE	 
(EE 
$strEE 
,EE 
TypeNameEE $
=EE% &
$strEE' 6
)EE6 7
]EE7 8
publicFF 
requiredFF 
decimalFF 
LongitudFF  (
{FF) *
getFF+ .
;FF. /
setFF0 3
;FF3 4
}FF5 6
[HH 	
RequiredHH	 
(HH 
ErrorMessageHH 
=HH  
$strHH! D
)HHD E
]HHE F
[II 	
ColumnII	 
(II 
$strII 
)II 
]II 
publicJJ 
requiredJJ 
intJJ 
	IdUsuarioJJ %
{JJ& '
getJJ( +
;JJ+ ,
setJJ- 0
;JJ0 1
}JJ2 3
[LL 	
ColumnLL	 
(LL 
$strLL 
)LL 
]LL 
publicMM 
intMM 
IdEstadoMM 
{MM 
getMM !
;MM! "
setMM# &
;MM& '
}MM( )
=MM* +
$numMM, -
;MM- .
[OO 	
ColumnOO	 
(OO 
$strOO #
)OO# $
]OO$ %
publicPP 
DateTimePP 
FechaPublicacionPP (
{PP) *
getPP+ .
;PP. /
setPP0 3
;PP3 4
}PP5 6
=PP7 8
DateTimePP9 A
.PPA B
NowPPB E
;PPE F
[RR 	

ForeignKeyRR	 
(RR 
$strRR !
)RR! "
]RR" #
publicSS 
	LocalidadSS 
?SS 
	LocalidadSS #
{SS$ %
getSS& )
;SS) *
setSS+ .
;SS. /
}SS0 1
[UU 	

ForeignKeyUU	 
(UU 
$strUU 
)UU 
]UU 
publicVV 
TipoInmuebleVV 
?VV 
TipoInmuebleVV )
{VV* +
getVV, /
;VV/ 0
setVV1 4
;VV4 5
}VV6 7
[XX 	

ForeignKeyXX	 
(XX 
$strXX 
)XX 
]XX 
publicYY 
ModoTransaccionYY 
?YY 
ModoTransaccionYY  /
{YY0 1
getYY2 5
;YY5 6
setYY7 :
;YY: ;
}YY< =
[[[ 	

ForeignKey[[	 
([[ 
$str[[ 
)[[ 
][[  
public\\ 
EstadoPublicacion\\  
?\\  !
EstadoPublicacion\\" 3
{\\4 5
get\\6 9
;\\9 :
set\\; >
;\\> ?
}\\@ A
[^^ 	

ForeignKey^^	 
(^^ 
$str^^ 
)^^  
]^^  !
public__ 
Usuario__ 
?__ 
Usuario__ 
{__  !
get__" %
;__% &
set__' *
;__* +
}__, -
[aa 	
InversePropertyaa	 
(aa 
$straa #
)aa# $
]aa$ %
publicbb 
ICollectionbb 
<bb 
Imagenbb !
>bb! "
Imagenesbb# +
{bb, -
getbb. 1
;bb1 2
setbb3 6
;bb6 7
}bb8 9
=bb: ;
newbb< ?
Listbb@ D
<bbD E
ImagenbbE K
>bbK L
(bbL M
)bbM N
;bbN O
[dd 	
InversePropertydd	 
(dd 
$strdd #
)dd# $
]dd$ %
publicee 
ICollectionee 
<ee "
InmuebleCaracteristicaee 1
>ee1 2#
InmuebleCaracteristicasee3 J
{eeK L
geteeM P
;eeP Q
seteeR U
;eeU V
}eeW X
=eeY Z
newee[ ^
Listee_ c
<eec d"
InmuebleCaracteristicaeed z
>eez {
(ee{ |
)ee| }
;ee} ~
}ff 
}gg Å
UC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Models\Contacto.cs
	namespace 	
rever
 
. 
Models 
{ 
[ 
Table 

(
 
$str 
) 
] 
public 

class 
Contacto 
{		 
[

 	
Key

	 
]

 
[ 	
Column	 
( 
$str 
) 
] 
public 
int 

IdContacto 
{ 
get  #
;# $
set% (
;( )
}* +
[ 	
Required	 
( 
ErrorMessage 
=  
$str! ?
)? @
]@ A
[ 	
Column	 
( 
$str 
) 
]  
public 
int 
IdComprador 
{  
get! $
;$ %
set& )
;) *
}+ ,
[ 	
Required	 
( 
ErrorMessage 
=  
$str! >
)> ?
]? @
[ 	
Column	 
( 
$str 
) 
] 
public 
int 

IdVendedor 
{ 
get  #
;# $
set% (
;( )
}* +
[ 	
Required	 
( 
ErrorMessage 
=  
$str! >
)> ?
]? @
[ 	
Column	 
( 
$str 
) 
] 
public 
int 

IdInmueble 
{ 
get  #
;# $
set% (
;( )
}* +
[ 	
Required	 
( 
ErrorMessage 
=  
$str! =
)= >
]> ?
[ 	
	MinLength	 
( 
$num 
) 
] 
[ 	
Column	 
( 
$str 
, 
TypeName #
=$ %
$str& ,
), -
]- .
public 
string 
Mensaje 
{ 
get  #
;# $
set% (
;( )
}* +
[ 	
Column	 
( 
$str 
) 
] 
public   
DateTime   
Fecha   
{   
get    #
;  # $
set  % (
;  ( )
}  * +
=  , -
DateTime  . 6
.  6 7
Now  7 :
;  : ;
[$$ 	

ForeignKey$$	 
($$ 
$str$$ !
)$$! "
]$$" #
public%% 
Usuario%% 
?%% 
	Comprador%% !
{%%" #
get%%$ '
;%%' (
set%%) ,
;%%, -
}%%. /
['' 	

ForeignKey''	 
('' 
$str''  
)''  !
]''! "
public(( 
Usuario(( 
?(( 
Vendedor((  
{((! "
get((# &
;((& '
set((( +
;((+ ,
}((- .
[** 	

ForeignKey**	 
(** 
$str**  
)**  !
]**! "
public++ 
Inmueble++ 
?++ 
Inmueble++ !
{++" #
get++$ '
;++' (
set++) ,
;++, -
}++. /
},, 
}-- •	
SC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Models\Ciudad.cs
	namespace 	
rever
 
. 
Models 
{ 
[ 
Table 

(
 
$str 
) 
] 
public		 

class		 
Ciudad		 
{

 
[ 	
Key	 
] 
[ 	
Column	 
( 
$str 
) 
] 
public 
int 
IdCiudad 
{ 
get !
;! "
set# &
;& '
}( )
[ 	
Required	 
( 
ErrorMessage 
=  
$str! <
)< =
]= >
[ 	
StringLength	 
( 
$num 
) 
] 
[ 	
Column	 
( 
$str 
) 
] 
public 
required 
string 
Nombre %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
} 
} Ω	
[C:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Models\Caracteristica.cs
	namespace 	
rever
 
. 
Models 
{ 
[ 
Table 

(
 
$str 
) 
] 
public 

class 
Caracteristica 
{ 
[		 	
Key			 
]		 
[

 	
Column

	 
(

 
$str

 #
)

# $
]

$ %
public 
int 
IdCaracteristica #
{$ %
get& )
;) *
set+ .
;. /
}0 1
[ 	
Required	 
( 
ErrorMessage 
=  
$str! <
)< =
]= >
[ 	
StringLength	 
( 
$num 
) 
] 
[ 	
Column	 
( 
$str 
) 
] 
public 
required 
string 
Nombre %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
} 
} Ñ
]C:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Extensions\EmailService.cs
	namespace 	

Extensions
 
{ 
public 

	interface 
IEmailService "
{ 
Task 
SendEmailAsync 
( 
string "
toEmail# *
,* +
string, 2
subject3 :
,: ;
string< B
bodyC G
)G H
;H I
}		 
public 

class 
EmailService 
: 
IEmailService  -
{ 
private 
readonly 
IConfiguration '
_configuration( 6
;6 7
public 
EmailService 
( 
IConfiguration *
configuration+ 8
)8 9
{ 	
_configuration 
= 
configuration *
;* +
} 	
public 
async 
Task 
SendEmailAsync (
(( )
string) /
toEmail0 7
,7 8
string9 ?
subject@ G
,G H
stringI O
bodyP T
)T U
{ 	
var 
server 
= 
_configuration '
[' (
$str( =
]= >
;> ?
var 
port 
= 
int 
. 
Parse  
(  !
_configuration! /
[/ 0
$str0 C
]C D
??E G
$strH M
)M N
;N O
var 
senderEmail 
= 
_configuration ,
[, -
$str- G
]G H
;H I
var 

senderName 
= 
_configuration +
[+ ,
$str, E
]E F
;F G
var 
password 
= 
_configuration )
[) *
$str* A
]A B
;B C
using 
( 
var 

smtpClient !
=" #
new$ '

SmtpClient( 2
(2 3
server3 9
,9 :
port; ?
)? @
)@ A
{ 

smtpClient 
. 
Credentials &
=' (
new) ,
NetworkCredential- >
(> ?
senderEmail? J
,J K
passwordL T
)T U
;U V

smtpClient 
. 
	EnableSsl $
=% &
true' +
;+ ,
var!! 
mailMessage!! 
=!!  !
new!!" %
MailMessage!!& 1
{"" 
From## 
=## 
new## 
MailAddress## *
(##* +
senderEmail##+ 6
!##6 7
,##7 8

senderName##9 C
)##C D
,##D E
Subject$$ 
=$$ 
subject$$ %
,$$% &
Body%% 
=%% 
body%% 
,%%  

IsBodyHtml&& 
=&&  
true&&! %
}'' 
;'' 
mailMessage)) 
.)) 
To)) 
.)) 
Add)) "
())" #
toEmail))# *
)))* +
;))+ ,
await++ 

smtpClient++  
.++  !
SendMailAsync++! .
(++. /
mailMessage++/ :
)++: ;
;++; <
},, 
}-- 	
}.. 
}// å
jC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Extensions\ClaimsPrincipalExtensions.cs
	namespace 	
rever
 
{ 
public 

static 
class %
ClaimsPrincipalExtensions 1
{ 
public 
static 
int 
? 
ObtenerIdUsuario +
(+ ,
this, 0
ClaimsPrincipal1 @
userA E
)E F
{ 	
if		 
(		 
user		 
==		 
null		 
)		 
return		 $
null		% )
;		) *
var 
claim 
= 
user 
. 
	FindFirst &
(& '
$str' 2
)2 3
?? 
user 
. 
	FindFirst &
(& '

ClaimTypes' 1
.1 2
NameIdentifier2 @
)@ A
?? 
user 
. 
	FindFirst &
(& '
$str' ,
), -
?? 
user 
. 
	FindFirst &
(& '
$str' +
)+ ,
;, -
if 
( 
claim 
!= 
null 
&&  
int! $
.$ %
TryParse% -
(- .
claim. 3
.3 4
Value4 9
,9 :
out; >
int? B
idC E
)E F
)F G
{ 
return 
id 
; 
} 
return 
null 
; 
} 	
public 
static 
bool 
EsAdministrador *
(* +
this+ /
ClaimsPrincipal0 ?
user@ D
)D E
{ 	
return 
user 
. 
IsInRole  
(  !
$str! $
)$ %
;% &
} 	
public   
static   
bool   
EsDue√±oOAdmin   (
(  ( )
this  ) -
ClaimsPrincipal  . =
user  > B
,  B C
int  D G
idPropietario  H U
)  U V
{!! 	
var"" 
idUsuarioToken"" 
=""  
user""! %
.""% &
ObtenerIdUsuario""& 6
(""6 7
)""7 8
;""8 9
if## 
(## 
!## 
idUsuarioToken## 
.##  
HasValue##  (
)##( )
{$$ 
return%% 
false%% 
;%% 
}&& 
return(( 
idUsuarioToken(( !
.((! "
Value((" '
==((( *
idPropietario((+ 8
||((9 ;
user((< @
.((@ A
EsAdministrador((A P
(((P Q
)((Q R
;((R S
})) 	
}** 
}++ ◊
]C:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Dtos\InmuebleListadoDto.cs
	namespace 	
rever
 
. 
Dtos 
{ 
public 

class 
InmuebleListadoDto #
{ 
public 
int 

IdInmueble 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
Titulo 
{ 
get "
;" #
set$ '
;' (
}) *
public 
decimal 
Precio 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
	Ubicacion 
{  !
get" %
;% &
set' *
;* +
}, -
public		 
int		 
Habitaciones		 
{		  !
get		" %
;		% &
set		' *
;		* +
}		, -
public

 
int

 
Banos

 
{

 
get

 
;

 
set

  #
;

# $
}

% &
public 
double 
MetrosCuadrados %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
int 
Pisos 
{ 
get 
; 
set  #
;# $
}% &
public 
string 
Tipo 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
Modo 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
Ciudad 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
	Localidad 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string 
	ImagenUrl 
{  !
get" %
;% &
set' *
;* +
}, -
public 
List 
< 
string 
> 
Caracteristicas +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
} 
} ˆ
]C:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Dtos\InmuebleDetalleDto.cs
	namespace 	
rever
 
. 
Dtos 
{ 
public 

class 
InmuebleDetalleDto #
{ 
public 
int 

IdInmueble 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
Titulo 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
Descripcion !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
decimal 
Precio 
{ 
get  #
;# $
set% (
;( )
}* +
public		 
string		 
	Direccion		 
{		  !
get		" %
;		% &
set		' *
;		* +
}		, -
public

 
string

 
	Ubicacion

 
{

  !
get

" %
;

% &
set

' *
;

* +
}

, -
public 
int 
Habitaciones 
{  !
get" %
;% &
set' *
;* +
}, -
public 
int 
Banos 
{ 
get 
; 
set  #
;# $
}% &
public 
int 
MetrosCuadrados "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
int 
Pisos 
{ 
get 
; 
set  #
;# $
}% &
public 
int 
Estrato 
{ 
get  
;  !
set" %
;% &
}' (
public 
decimal 
Latitud 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
decimal 
Longitud 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string 
Tipo 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
Modo 
{ 
get  
;  !
set" %
;% &
}' (
public 
List 
< 
string 
> 
Imagenes $
{% &
get' *
;* +
set, /
;/ 0
}1 2
=3 4
new5 8
(8 9
)9 :
;: ;
public 
List 
< 
string 
> 
Caracteristicas +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
=: ;
new< ?
(? @
)@ A
;A B
public 
int 

IdVendedor 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
NombreVendedor $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
string 
TelefonoVendedor &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
string 
CorreoVendedor $
{% &
get' *
;* +
set, /
;/ 0
}1 2
} 
} ∑1
[C:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Dtos\CrearInmuebleDto.cs
	namespace 	

TuProyecto
 
. 
DTOs 
{ 
public 

class 
CrearInmuebleDto !
{ 
[ 	
Required	 
( 
ErrorMessage 
=  
$str! <
)< =
]= >
[		 	
StringLength			 
(		 
$num		 
,		 
ErrorMessage		 '
=		( )
$str		* Z
)		Z [
]		[ \
public

 
required

 
string

 
Titulo

 %
{

& '
get

( +
;

+ ,
set

- 0
;

0 1
}

2 3
[ 	
Required	 
( 
ErrorMessage 
=  
$str! A
)A B
]B C
public 
required 
string 
Descripcion *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
[ 	
Range	 
( 
$num 
, 
$num 
,  
ErrorMessage! -
=. /
$str0 W
)W X
]X Y
public 
decimal 
Precio 
{ 
get  #
;# $
set% (
;( )
}* +
[ 	
Range	 
( 
$num 
, 
int 
. 
MaxValue 
, 
ErrorMessage  ,
=- .
$str/ W
)W X
]X Y
public 
int 
IdTipo 
{ 
get 
;  
set! $
;$ %
}& '
[ 	
Range	 
( 
$num 
, 
int 
. 
MaxValue 
, 
ErrorMessage  ,
=- .
$str/ Z
)Z [
][ \
public 
int 
IdModo 
{ 
get 
;  
set! $
;$ %
}& '
[ 	
Required	 
( 
ErrorMessage 
=  
$str! ?
)? @
]@ A
[ 	
StringLength	 
( 
$num 
, 
ErrorMessage '
=( )
$str* ]
)] ^
]^ _
public 
required 
string 
	Direccion (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
[ 	
Range	 
( 
$num 
, 
int 
. 
MaxValue 
, 
ErrorMessage  ,
=- .
$str/ Q
)Q R
]R S
public 
int 
IdLocalidad 
{  
get! $
;$ %
set& )
;) *
}+ ,
[   	
Range  	 
(   
$num   
,   
$num   
,   
ErrorMessage   "
=  # $
$str  % Q
)  Q R
]  R S
public!! 
int!! 
Habitaciones!! 
{!!  !
get!!" %
;!!% &
set!!' *
;!!* +
}!!, -
[$$ 	
JsonPropertyName$$	 
($$ 
$str$$ !
)$$! "
]$$" #
[%% 	
Range%%	 
(%% 
$num%% 
,%% 
$num%% 
,%% 
ErrorMessage%% "
=%%# $
$str%%% J
)%%J K
]%%K L
public&& 
int&& 
Banos&& 
{&& 
get&& 
;&& 
set&&  #
;&&# $
}&&% &
[)) 	
JsonPropertyName))	 
()) 
$str))  
)))  !
]))! "
[** 	
Range**	 
(** 
$num** 
,** 
$num** 
,** 
ErrorMessage** #
=**$ %
$str**& J
)**J K
]**K L
public++ 
int++ 
Pisos++ 
{++ 
get++ 
;++ 
set++  #
;++# $
}++% &
=++' (
$num++) *
;++* +
[-- 	
Range--	 
(-- 
$num-- 
,-- 
int-- 
.-- 
MaxValue-- 
,-- 
ErrorMessage--  ,
=--- .
$str--/ \
)--\ ]
]--] ^
public.. 
int.. 
MetrosCuadrados.. "
{..# $
get..% (
;..( )
set..* -
;..- .
}../ 0
[00 	
Range00	 
(00 
$num00 
,00 
$num00 
,00 
ErrorMessage00 !
=00" #
$str00$ H
)00H I
]00I J
public11 
int11 
Estrato11 
{11 
get11  
;11  !
set11" %
;11% &
}11' (
public33 
decimal33 
Latitud33 
{33  
get33! $
;33$ %
set33& )
;33) *
}33+ ,
=33- .
$num33/ 0
;330 1
public44 
decimal44 
Longitud44 
{44  !
get44" %
;44% &
set44' *
;44* +
}44, -
=44. /
$num440 1
;441 2
public77 
List77 
<77 
int77 
>77 
CaracteristicasIds77 +
{77, -
get77. 1
;771 2
set773 6
;776 7
}778 9
=77: ;
new77< ?
(77? @
)77@ A
;77A B
[99 	
JsonPropertyName99	 
(99 
$str99 (
)99( )
]99) *
public:: 
List:: 
<:: 
string:: 
>:: 
ImagenesUrls:: (
{::) *
get::+ .
;::. /
set::0 3
;::3 4
}::5 6
=::7 8
new::9 <
(::< =
)::= >
;::> ?
};; 
}<< ◊™
cC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Controllers\UsuarioController.cs
	namespace

 	
rever


 
.

 
Controllers

 
{ 
[ 
Route 

(
 
$str 
) 
] 
[ 
ApiController 
] 
[ 
	Authorize 
( 
Roles 
= 
$str 
) 
]  
public 

class 
UsuarioController "
:# $
ControllerBase% 3
{ 
private 
readonly 
IUsuarioRepository +
_Usuariorrepository, ?
;? @
public 
UsuarioController  
(  !
IUsuarioRepository! 3

repository4 >
)> ?
{ 	
_Usuariorrepository 
=  !

repository" ,
;, -
} 	
[ 	
HttpGet	 
] 
[ 	
	Authorize	 
( 
Roles 
= 
$str 
) 
]  
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *
Status200OK* 5
)5 6
]6 7
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *
Status400BadRequest* =
)= >
]> ?
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *!
Status401Unauthorized* ?
)? @
]@ A
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *
Status404NotFound* ;
); <
]< =
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *(
Status500InternalServerError* F
)F G
]G H
public 
async 
Task 
< 
IActionResult '
>' (
ListarUsuario) 6
(6 7
)7 8
{   	
try!! 
{"" 
if## 
(## 
User## 
.## 
Identity## !
==##" $
null##% )
||##* ,
!##- .
User##. 2
.##2 3
Identity##3 ;
.##; <
IsAuthenticated##< K
)##K L
{$$ 
return%% 

StatusCode%% %
(%%% &
$num%%& )
,%%) *
$str%%+ I
)%%I J
;%%J K
}&& 
var(( 
response(( 
=(( 
await(( $
_Usuariorrepository((% 8
.((8 9

GetUsuario((9 C
(((C D
)((D E
;((E F
if** 
(** 
response** 
==** 
null**  $
)**$ %
{++ 
return,, 

StatusCode,, %
(,,% &
$num,,& )
,,,) *
$str,,+ Y
),,Y Z
;,,Z [
}-- 
return// 

StatusCode// !
(//! "
$num//" %
,//% &
response//' /
)/// 0
;//0 1
}00 
catch11 
(11 
	Exception11 
)11 
{22 
return33 

StatusCode33 !
(33! "
$num33" %
,33% &
$str33' I
)33I J
;33J K
}44 
}55 	
[77 	
HttpGet77	 
(77 
$str77 
)77 
]77 
[88 	 
ProducesResponseType88	 
(88 
StatusCodes88 )
.88) *
Status200OK88* 5
)885 6
]886 7
[99 	 
ProducesResponseType99	 
(99 
StatusCodes99 )
.99) *
Status400BadRequest99* =
)99= >
]99> ?
[:: 	 
ProducesResponseType::	 
(:: 
StatusCodes:: )
.::) *!
Status401Unauthorized::* ?
)::? @
]::@ A
[;; 	 
ProducesResponseType;;	 
(;; 
StatusCodes;; )
.;;) *
Status404NotFound;;* ;
);;; <
];;< =
[<< 	 
ProducesResponseType<<	 
(<< 
StatusCodes<< )
.<<) *(
Status500InternalServerError<<* F
)<<F G
]<<G H
public== 
async== 
Task== 
<== 
IActionResult== '
>==' (
ObtenerUsuario==) 7
(==7 8
int==8 ;
id==< >
)==> ?
{>> 	
try?? 
{@@ 
varAA 
idUsuarioTokenAA "
=AA# $
UserAA% )
.AA) *
ObtenerIdUsuarioAA* :
(AA: ;
)AA; <
;AA< =
varBB 
esadministradorBB #
=BB$ %
UserBB& *
.BB* +
IsInRoleBB+ 3
(BB3 4
$strBB4 C
)BBC D
;BBD E
ifDD 
(DD 
idDD 
<=DD 
$numDD 
)DD 
{EE 
returnFF 

StatusCodeFF %
(FF% &
$numFF& )
,FF) *
$strFF+ S
)FFS T
;FFT U
}GG 
ifII 
(II 
idII 
!=II 
idUsuarioTokenII (
&&II) +
!II, -
esadministradorII- <
)II< =
{JJ 
returnKK 

StatusCodeKK %
(KK% &
$numKK& )
,KK) *
$strKK+ Z
)KKZ [
;KK[ \
}LL 
ifNN 
(NN 
UserNN 
.NN 
IdentityNN !
==NN" $
nullNN% )
||NN* ,
!NN- .
UserNN. 2
.NN2 3
IdentityNN3 ;
.NN; <
IsAuthenticatedNN< K
)NNK L
{OO 
returnPP 

StatusCodePP %
(PP% &
$numPP& )
,PP) *
$strPP+ I
)PPI J
;PPJ K
}QQ 
varSS 
existSS 
=SS 
awaitSS !
_UsuariorrepositorySS" 5
.SS5 6
GetUsuarioByIdSS6 D
(SSD E
idSSE G
)SSG H
;SSH I
ifUU 
(UU 
existUU 
==UU 
nullUU !
)UU! "
{VV 
returnWW 

StatusCodeWW %
(WW% &
$numWW& )
,WW) *
$"WW+ -
$strWW- S
{WWS T
idWWT V
}WWV W
$strWWW X
"WWX Y
)WWY Z
;WWZ [
}XX 
returnZZ 

StatusCodeZZ !
(ZZ! "
$numZZ" %
,ZZ% &
existZZ' ,
)ZZ, -
;ZZ- .
}[[ 
catch\\ 
(\\ 
	Exception\\ 
ex\\ 
)\\  
{]] 
return^^ 

StatusCode^^ !
(^^! "
$num^^" %
,^^% &
$"^^' )
$str^^) S
{^^S T
ex^^T V
.^^V W
Message^^W ^
}^^^ _
$str^^_ i
{^^i j
ex^^j l
.^^l m
InnerException^^m {
?^^{ |
.^^| }
Message	^^} Ñ
}
^^Ñ Ö
"
^^Ö Ü
)
^^Ü á
;
^^á à
}__ 
}`` 	
[bb 	
HttpPostbb	 
]bb 
[cc 	
AllowAnonymouscc	 
]cc 
[dd 	 
ProducesResponseTypedd	 
(dd 
StatusCodesdd )
.dd) *
Status200OKdd* 5
)dd5 6
]dd6 7
[ee 	 
ProducesResponseTypeee	 
(ee 
StatusCodesee )
.ee) *
Status400BadRequestee* =
)ee= >
]ee> ?
[ff 	 
ProducesResponseTypeff	 
(ff 
StatusCodesff )
.ff) *!
Status401Unauthorizedff* ?
)ff? @
]ff@ A
[gg 	 
ProducesResponseTypegg	 
(gg 
StatusCodesgg )
.gg) *
Status403Forbiddengg* <
)gg< =
]gg= >
[hh 	 
ProducesResponseTypehh	 
(hh 
StatusCodeshh )
.hh) *(
Status500InternalServerErrorhh* F
)hhF G
]hhG H
publicii 
asyncii 
Taskii 
<ii 
IActionResultii '
>ii' (
CrearUsuarioii) 5
(ii5 6
[ii6 7
FromBodyii7 ?
]ii? @
UsuarioiiA H
usuarioiiI P
)iiP Q
{jj 	
trykk 
{ll 
ifmm 
(mm 
usuariomm 
==mm 
nullmm #
||mm$ &
stringmm' -
.mm- .
IsNullOrWhiteSpacemm. @
(mm@ A
usuariommA H
.mmH I
CorreommI O
)mmO P
||mmQ S
stringmmT Z
.mmZ [
IsNullOrWhiteSpacemm[ m
(mmm n
usuariommn u
.mmu v
Contrase√±a	mmv Ä
)
mmÄ Å
)
mmÅ Ç
{nn 
returnoo 

StatusCodeoo %
(oo% &
$numoo& )
,oo) *
$stroo+ o
)ooo p
;oop q
}pp 
varss 
usuarioExistentess $
=ss% &
awaitss' ,
_Usuariorrepositoryss- @
.ss@ A"
GetByEmailWithRolAsyncssA W
(ssW X
usuariossX _
.ss_ `
Correoss` f
)ssf g
;ssg h
iftt 
(tt 
usuarioExistentett $
!=tt% '
nulltt( ,
)tt, -
{uu 
returnvv 

StatusCodevv %
(vv% &
$numvv& )
,vv) *
$strvv+ W
)vvW X
;vvX Y
}ww 
boolzz 
esadministradorzz $
=zz% &
Userzz' +
.zz+ ,
EsAdministradorzz, ;
(zz; <
)zz< =
;zz= >
if|| 
(|| 
!|| 
esadministrador|| $
&&||% '
usuario||( /
.||/ 0
IdRol||0 5
==||6 8
$num||9 :
)||: ;
{}} 
return~~ 

StatusCode~~ %
(~~% &
$num~~& )
,~~) *
$str~~+ c
)~~c d
;~~d e
} 
if
ÇÇ 
(
ÇÇ 
!
ÇÇ 
esadministrador
ÇÇ $
&&
ÇÇ% '
(
ÇÇ( )
usuario
ÇÇ) 0
.
ÇÇ0 1
IdRol
ÇÇ1 6
!=
ÇÇ7 9
$num
ÇÇ: ;
&&
ÇÇ< >
usuario
ÇÇ? F
.
ÇÇF G
IdRol
ÇÇG L
!=
ÇÇM O
$num
ÇÇP Q
)
ÇÇQ R
)
ÇÇR S
{
ÉÉ 
return
ÑÑ 

StatusCode
ÑÑ %
(
ÑÑ% &
$num
ÑÑ& )
,
ÑÑ) *
$str
ÑÑ+ h
)
ÑÑh i
;
ÑÑi j
}
ÖÖ 
usuario
àà 
.
àà 
Contrase√±a
àà "
=
àà# $
BCrypt
àà% +
.
àà+ ,
Net
àà, /
.
àà/ 0
BCrypt
àà0 6
.
àà6 7
HashPassword
àà7 C
(
ààC D
usuario
ààD K
.
ààK L
Contrase√±a
ààL V
)
ààV W
;
ààW X
var
ää 
response
ää 
=
ää 
await
ää $!
_Usuariorrepository
ää% 8
.
ää8 9
PostUsuario
ää9 D
(
ääD E
usuario
ääE L
)
ääL M
;
ääM N
if
åå 
(
åå 
!
åå 
response
åå 
)
åå 
{
çç 
return
éé 

StatusCode
éé %
(
éé% &
$num
éé& )
,
éé) *
$str
éé+ ]
)
éé] ^
;
éé^ _
}
èè 
usuario
ëë 
.
ëë 
Contrase√±a
ëë "
=
ëë# $
null
ëë% )
;
ëë) *
return
ìì 

StatusCode
ìì !
(
ìì! "
$num
ìì" %
,
ìì% &
usuario
ìì' .
)
ìì. /
;
ìì/ 0
}
îî 
catch
ïï 
(
ïï 
	Exception
ïï 
)
ïï 
{
ññ 
return
óó 

StatusCode
óó !
(
óó! "
$num
óó" %
,
óó% &
$str
óó' I
)
óóI J
;
óóJ K
}
òò 
}
ôô 	
[
õõ 	
HttpPut
õõ	 
]
õõ 
[
úú 	"
ProducesResponseType
úú	 
(
úú 
StatusCodes
úú )
.
úú) *
Status200OK
úú* 5
)
úú5 6
]
úú6 7
[
ùù 	"
ProducesResponseType
ùù	 
(
ùù 
StatusCodes
ùù )
.
ùù) *!
Status400BadRequest
ùù* =
)
ùù= >
]
ùù> ?
[
ûû 	"
ProducesResponseType
ûû	 
(
ûû 
StatusCodes
ûû )
.
ûû) *#
Status401Unauthorized
ûû* ?
)
ûû? @
]
ûû@ A
[
üü 	"
ProducesResponseType
üü	 
(
üü 
StatusCodes
üü )
.
üü) * 
Status403Forbidden
üü* <
)
üü< =
]
üü= >
[
†† 	"
ProducesResponseType
††	 
(
†† 
StatusCodes
†† )
.
††) *
Status404NotFound
††* ;
)
††; <
]
††< =
[
°° 	"
ProducesResponseType
°°	 
(
°° 
StatusCodes
°° )
.
°°) **
Status500InternalServerError
°°* F
)
°°F G
]
°°G H
public
¢¢ 
async
¢¢ 
Task
¢¢ 
<
¢¢ 
IActionResult
¢¢ '
>
¢¢' (
ActualizarUsuario
¢¢) :
(
¢¢: ;
[
¢¢; <
FromBody
¢¢< D
]
¢¢D E
Usuario
¢¢F M
usuario
¢¢N U
)
¢¢U V
{
££ 	
try
§§ 
{
•• 
if
ßß 
(
ßß 
usuario
ßß 
==
ßß 
null
ßß #
||
ßß$ &
usuario
ßß' .
.
ßß. /
	IdUsuario
ßß/ 8
<=
ßß9 ;
$num
ßß< =
)
ßß= >
{
®® 
return
©© 

StatusCode
©© %
(
©©% &
$num
©©& )
,
©©) *
$str
©©+ c
)
©©c d
;
©©d e
}
™™ 
if
≠≠ 
(
≠≠ 
!
≠≠ 
User
≠≠ 
.
≠≠ 
EsDue√±oOAdmin
≠≠ '
(
≠≠' (
usuario
≠≠( /
.
≠≠/ 0
	IdUsuario
≠≠0 9
)
≠≠9 :
)
≠≠: ;
{
ÆÆ 
return
ØØ 

StatusCode
ØØ %
(
ØØ% &
$num
ØØ& )
,
ØØ) *
$str
ØØ+ a
)
ØØa b
;
ØØb c
}
∞∞ 
var
≥≥ 
exist
≥≥ 
=
≥≥ 
await
≥≥ !!
_Usuariorrepository
≥≥" 5
.
≥≥5 6
GetUsuarioById
≥≥6 D
(
≥≥D E
usuario
≥≥E L
.
≥≥L M
	IdUsuario
≥≥M V
)
≥≥V W
;
≥≥W X
if
µµ 
(
µµ 
exist
µµ 
==
µµ 
null
µµ !
)
µµ! "
{
∂∂ 
return
∑∑ 

StatusCode
∑∑ %
(
∑∑% &
$num
∑∑& )
,
∑∑) *
$"
∑∑+ -
$str
∑∑- \
{
∑∑\ ]
usuario
∑∑] d
.
∑∑d e
	IdUsuario
∑∑e n
}
∑∑n o
$str
∑∑o z
"
∑∑z {
)
∑∑{ |
;
∑∑| }
}
∏∏ 
exist
ªª 
.
ªª 
Nombre
ªª 
=
ªª 
usuario
ªª &
.
ªª& '
Nombre
ªª' -
;
ªª- .
exist
ºº 
.
ºº 
Correo
ºº 
=
ºº 
usuario
ºº &
.
ºº& '
Correo
ºº' -
;
ºº- .
exist
ΩΩ 
.
ΩΩ 
Telefono
ΩΩ 
=
ΩΩ  
usuario
ΩΩ! (
.
ΩΩ( )
Telefono
ΩΩ) 1
;
ΩΩ1 2
exist
ææ 
.
ææ 
IdRol
ææ 
=
ææ 
usuario
ææ %
.
ææ% &
IdRol
ææ& +
;
ææ+ ,
if
¿¿ 
(
¿¿ 
!
¿¿ 
string
¿¿ 
.
¿¿  
IsNullOrWhiteSpace
¿¿ .
(
¿¿. /
usuario
¿¿/ 6
.
¿¿6 7
Contrase√±a
¿¿7 A
)
¿¿A B
)
¿¿B C
{
¡¡ 
exist
¬¬ 
.
¬¬ 
Contrase√±a
¬¬ $
=
¬¬% &
BCrypt
¬¬' -
.
¬¬- .
Net
¬¬. 1
.
¬¬1 2
BCrypt
¬¬2 8
.
¬¬8 9
HashPassword
¬¬9 E
(
¬¬E F
usuario
¬¬F M
.
¬¬M N
Contrase√±a
¬¬N X
)
¬¬X Y
;
¬¬Y Z
}
√√ 
var
≈≈ 
response
≈≈ 
=
≈≈ 
await
≈≈ $!
_Usuariorrepository
≈≈% 8
.
≈≈8 9

PutUsuario
≈≈9 C
(
≈≈C D
exist
≈≈D I
)
≈≈I J
;
≈≈J K
exist
»» 
.
»» 
Contrase√±a
»»  
=
»»! "
null
»»# '
!
»»' (
;
»»( )
return
   

StatusCode
   !
(
  ! "
$num
  " %
,
  % &
exist
  ' ,
)
  , -
;
  - .
}
ÀÀ 
catch
ÃÃ 
(
ÃÃ 
	Exception
ÃÃ 
ex
ÃÃ 
)
ÃÃ  
{
ÕÕ 
return
ŒŒ 

StatusCode
ŒŒ !
(
ŒŒ! "
$num
ŒŒ" %
,
ŒŒ% &
$"
ŒŒ' )
$str
ŒŒ) S
{
ŒŒS T
ex
ŒŒT V
.
ŒŒV W
Message
ŒŒW ^
}
ŒŒ^ _
"
ŒŒ_ `
)
ŒŒ` a
;
ŒŒa b
}
œœ 
}
–– 	
[
““ 	

HttpDelete
““	 
(
““ 
$str
““ 
)
““ 
]
““ 
[
”” 	"
ProducesResponseType
””	 
(
”” 
StatusCodes
”” )
.
””) *
Status200OK
””* 5
)
””5 6
]
””6 7
[
‘‘ 	"
ProducesResponseType
‘‘	 
(
‘‘ 
StatusCodes
‘‘ )
.
‘‘) *!
Status400BadRequest
‘‘* =
)
‘‘= >
]
‘‘> ?
[
’’ 	"
ProducesResponseType
’’	 
(
’’ 
StatusCodes
’’ )
.
’’) *#
Status401Unauthorized
’’* ?
)
’’? @
]
’’@ A
[
÷÷ 	"
ProducesResponseType
÷÷	 
(
÷÷ 
StatusCodes
÷÷ )
.
÷÷) *
Status404NotFound
÷÷* ;
)
÷÷; <
]
÷÷< =
[
◊◊ 	"
ProducesResponseType
◊◊	 
(
◊◊ 
StatusCodes
◊◊ )
.
◊◊) **
Status500InternalServerError
◊◊* F
)
◊◊F G
]
◊◊G H
public
ÿÿ 
async
ÿÿ 
Task
ÿÿ 
<
ÿÿ 
IActionResult
ÿÿ '
>
ÿÿ' (
DeleteUsuario
ÿÿ) 6
(
ÿÿ6 7
int
ÿÿ7 :
id
ÿÿ; =
)
ÿÿ= >
{
ŸŸ 	
try
⁄⁄ 
{
€€ 
if
‹‹ 
(
‹‹ 
id
‹‹ 
<=
‹‹ 
$num
‹‹ 
)
‹‹ 
{
›› 
return
ﬁﬁ 

StatusCode
ﬁﬁ %
(
ﬁﬁ% &
$num
ﬁﬁ& )
,
ﬁﬁ) *
$str
ﬁﬁ+ Q
)
ﬁﬁQ R
;
ﬁﬁR S
}
ﬂﬂ 
var
‚‚ 
idUsuarioToken
‚‚ "
=
‚‚# $
User
‚‚% )
.
‚‚) *
ObtenerIdUsuario
‚‚* :
(
‚‚: ;
)
‚‚; <
;
‚‚< =
if
‰‰ 
(
‰‰ 
!
‰‰ 
idUsuarioToken
‰‰ #
.
‰‰# $
HasValue
‰‰$ ,
)
‰‰, -
{
ÂÂ 
return
ÊÊ 

StatusCode
ÊÊ %
(
ÊÊ% &
$num
ÊÊ& )
,
ÊÊ) *
$str
ÊÊ+ c
)
ÊÊc d
;
ÊÊd e
}
ÁÁ 
var
ÈÈ 
esAdmin
ÈÈ 
=
ÈÈ 
User
ÈÈ "
.
ÈÈ" #
IsInRole
ÈÈ# +
(
ÈÈ+ ,
$str
ÈÈ, ;
)
ÈÈ; <
||
ÈÈ= ?
User
ÈÈ@ D
.
ÈÈD E
IsInRole
ÈÈE M
(
ÈÈM N
$str
ÈÈN U
)
ÈÈU V
;
ÈÈV W
if
ÏÏ 
(
ÏÏ 
!
ÏÏ 
User
ÏÏ 
.
ÏÏ 
EsDue√±oOAdmin
ÏÏ '
(
ÏÏ' (
id
ÏÏ( *
)
ÏÏ* +
)
ÏÏ+ ,
{
ÌÌ 
return
ÓÓ 

StatusCode
ÓÓ %
(
ÓÓ% &
$num
ÓÓ& )
,
ÓÓ) *
$str
ÓÓ+ _
)
ÓÓ_ `
;
ÓÓ` a
}
ÔÔ 
var
ÒÒ 
exist
ÒÒ 
=
ÒÒ 
await
ÒÒ !!
_Usuariorrepository
ÒÒ" 5
.
ÒÒ5 6
GetUsuarioById
ÒÒ6 D
(
ÒÒD E
id
ÒÒE G
)
ÒÒG H
;
ÒÒH I
if
ÛÛ 
(
ÛÛ 
exist
ÛÛ 
==
ÛÛ 
null
ÛÛ !
)
ÛÛ! "
{
ÙÙ 
return
ıı 

StatusCode
ıı %
(
ıı% &
$num
ıı& )
,
ıı) *
$"
ıı+ -
$str
ıı- Z
{
ııZ [
id
ıı[ ]
}
ıı] ^
$str
ıı^ i
"
ııi j
)
ııj k
;
ıık l
}
ˆˆ 
var
¯¯ 
response
¯¯ 
=
¯¯ 
await
¯¯ $!
_Usuariorrepository
¯¯% 8
.
¯¯8 9
DeleteUsuario
¯¯9 F
(
¯¯F G
exist
¯¯G L
)
¯¯L M
;
¯¯M N
return
˙˙ 

StatusCode
˙˙ !
(
˙˙! "
$num
˙˙" %
,
˙˙% &
response
˙˙' /
)
˙˙/ 0
;
˙˙0 1
}
˚˚ 
catch
¸¸ 
(
¸¸ 
	Exception
¸¸ 
)
¸¸ 
{
˝˝ 
return
˛˛ 

StatusCode
˛˛ !
(
˛˛! "
$num
˛˛" %
,
˛˛% &
$str
˛˛' I
)
˛˛I J
;
˛˛J K
}
ˇˇ 
}
ÄÄ 	
}
ÅÅ 
}ÇÇ ì{
gC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Controllers\TipoInmuebleControler.cs
	namespace

 	
rever


 
.

 
Controllers

 
{ 
[ 
Route 

(
 
$str 
) 
] 
[ 
ApiController 
] 
[ 
	Authorize 
( 
Roles 
= 
$str 
) 
] 
public 

class "
TipoInmuebleController '
:( )
ControllerBase* 8
{ 
private 
readonly #
ITipoInmuebleRepository 0#
_tipoinmueblerepository1 H
;H I
public "
TipoInmuebleController %
(% &#
ITipoInmuebleRepository& =

repository> H
)H I
{ 	#
_tipoinmueblerepository #
=$ %

repository& 0
;0 1
} 	
[ 	
HttpGet	 
] 
[ 	
AllowAnonymous	 
] 
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *
Status200OK* 5
)5 6
]6 7
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *
Status400BadRequest* =
)= >
]> ?
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *!
Status401Unauthorized* ?
)? @
]@ A
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *
Status404NotFound* ;
); <
]< =
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *(
Status500InternalServerError* F
)F G
]G H
public 
async 
Task 
< 
IActionResult '
>' (
ListarTipoInmueble) ;
(; <
)< =
{   	
try!! 
{"" 
var## 
response## 
=## 
await## $#
_tipoinmueblerepository##% <
.##< =
GetTipoInmueble##= L
(##L M
)##M N
;##N O
if%% 
(%% 
response%% 
==%% 
null%%  $
)%%$ %
{&& 
return'' 

StatusCode'' %
(''% &
$num''& )
,'') *
$str''+ b
)''b c
;''c d
}(( 
return** 

StatusCode** !
(**! "
$num**" %
,**% &
response**' /
)**/ 0
;**0 1
}++ 
catch,, 
(,, 
	Exception,, 
),, 
{-- 
return.. 

StatusCode.. !
(..! "
$num.." %
,..% &
$str..' I
)..I J
;..J K
}// 
}00 	
[22 	
HttpGet22	 
(22 
$str22 
)22 
]22 
[33 	
AllowAnonymous33	 
]33 
[44 	 
ProducesResponseType44	 
(44 
StatusCodes44 )
.44) *
Status200OK44* 5
)445 6
]446 7
[55 	 
ProducesResponseType55	 
(55 
StatusCodes55 )
.55) *
Status400BadRequest55* =
)55= >
]55> ?
[66 	 
ProducesResponseType66	 
(66 
StatusCodes66 )
.66) *!
Status401Unauthorized66* ?
)66? @
]66@ A
[77 	 
ProducesResponseType77	 
(77 
StatusCodes77 )
.77) *
Status404NotFound77* ;
)77; <
]77< =
[88 	 
ProducesResponseType88	 
(88 
StatusCodes88 )
.88) *(
Status500InternalServerError88* F
)88F G
]88G H
public99 
async99 
Task99 
<99 
IActionResult99 '
>99' (
ObtenerTipoInmueble99) <
(99< =
int99= @
id99A C
)99C D
{:: 	
try;; 
{<< 
if== 
(== 
id== 
<=== 
$num== 
)== 
{>> 
return?? 

StatusCode?? %
(??% &
$num??& )
,??) *
$str??+ S
)??S T
;??T U
}@@ 
varBB 
existBB 
=BB 
awaitBB !#
_tipoinmueblerepositoryBB" 9
.BB9 :
GetTipoInmuebleByIdBB: M
(BBM N
idBBN P
)BBP Q
;BBQ R
ifDD 
(DD 
existDD 
==DD 
nullDD !
)DD! "
{EE 
returnFF 

StatusCodeFF %
(FF% &
$numFF& )
,FF) *
$"FF+ -
$strFF- \
{FF\ ]
idFF] _
}FF_ `
$strFF` a
"FFa b
)FFb c
;FFc d
}GG 
returnII 

StatusCodeII !
(II! "
$numII" %
,II% &
existII' ,
)II, -
;II- .
}JJ 
catchKK 
(KK 
	ExceptionKK 
)KK 
{LL 
returnMM 

StatusCodeMM !
(MM! "
$numMM" %
,MM% &
$strMM' I
)MMI J
;MMJ K
}NN 
}OO 	
[QQ 	
HttpPostQQ	 
]QQ 
[RR 	 
ProducesResponseTypeRR	 
(RR 
StatusCodesRR )
.RR) *
Status200OKRR* 5
)RR5 6
]RR6 7
[SS 	 
ProducesResponseTypeSS	 
(SS 
StatusCodesSS )
.SS) *
Status400BadRequestSS* =
)SS= >
]SS> ?
[TT 	 
ProducesResponseTypeTT	 
(TT 
StatusCodesTT )
.TT) *!
Status401UnauthorizedTT* ?
)TT? @
]TT@ A
[UU 	 
ProducesResponseTypeUU	 
(UU 
StatusCodesUU )
.UU) *
Status404NotFoundUU* ;
)UU; <
]UU< =
[VV 	 
ProducesResponseTypeVV	 
(VV 
StatusCodesVV )
.VV) *(
Status500InternalServerErrorVV* F
)VVF G
]VVG H
publicWW 
asyncWW 
TaskWW 
<WW 
IActionResultWW '
>WW' (
CrearTipoInmuebleWW) :
(WW: ;
[WW; <
FromBodyWW< D
]WWD E
TipoInmuebleWWF R
tipoInmuebleWWS _
)WW_ `
{XX 	
tryYY 
{ZZ 
if[[ 
([[ 
User[[ 
.[[ 
Identity[[ !
==[[" $
null[[% )
||[[* ,
![[- .
User[[. 2
.[[2 3
Identity[[3 ;
.[[; <
IsAuthenticated[[< K
)[[K L
{\\ 
return]] 

StatusCode]] %
(]]% &
$num]]& )
,]]) *
$str]]+ I
)]]I J
;]]J K
}^^ 
if`` 
(`` 
tipoInmueble``  
==``! #
null``$ (
)``( )
{aa 
returnbb 

StatusCodebb %
(bb% &
$numbb& )
,bb) *
$strbb+ e
)bbe f
;bbf g
}cc 
varee 
responseee 
=ee 
awaitee $#
_tipoinmueblerepositoryee% <
.ee< =
PostTipoInmuebleee= M
(eeM N
tipoInmuebleeeN Z
)eeZ [
;ee[ \
ifgg 
(gg 
responsegg 
==gg 
nullgg  $
)gg$ %
{hh 
returnii 

StatusCodeii %
(ii% &
$numii& )
,ii) *
$strii+ ]
)ii] ^
;ii^ _
}jj 
returnll 

StatusCodell !
(ll! "
$numll" %
,ll% &
responsell' /
)ll/ 0
;ll0 1
}mm 
catchnn 
(nn 
	Exceptionnn 
)nn 
{oo 
returnpp 

StatusCodepp !
(pp! "
$numpp" %
,pp% &
$strpp' I
)ppI J
;ppJ K
}qq 
}rr 	
[tt 	
HttpPuttt	 
]tt 
[uu 	 
ProducesResponseTypeuu	 
(uu 
StatusCodesuu )
.uu) *
Status200OKuu* 5
)uu5 6
]uu6 7
[vv 	 
ProducesResponseTypevv	 
(vv 
StatusCodesvv )
.vv) *
Status400BadRequestvv* =
)vv= >
]vv> ?
[ww 	 
ProducesResponseTypeww	 
(ww 
StatusCodesww )
.ww) *!
Status401Unauthorizedww* ?
)ww? @
]ww@ A
[xx 	 
ProducesResponseTypexx	 
(xx 
StatusCodesxx )
.xx) *
Status404NotFoundxx* ;
)xx; <
]xx< =
[yy 	 
ProducesResponseTypeyy	 
(yy 
StatusCodesyy )
.yy) *(
Status500InternalServerErroryy* F
)yyF G
]yyG H
publiczz 
asynczz 
Taskzz 
<zz 
IActionResultzz '
>zz' ("
ActualizarTipoInmueblezz) ?
(zz? @
[zz@ A
FromBodyzzA I
]zzI J
TipoInmueblezzK W
tipoInmueblezzX d
)zzd e
{{{ 	
try|| 
{}} 
if~~ 
(~~ 
User~~ 
.~~ 
Identity~~ !
==~~" $
null~~% )
||~~* ,
!~~- .
User~~. 2
.~~2 3
Identity~~3 ;
.~~; <
IsAuthenticated~~< K
)~~K L
{ 
return
ÄÄ 

StatusCode
ÄÄ %
(
ÄÄ% &
$num
ÄÄ& )
,
ÄÄ) *
$str
ÄÄ+ I
)
ÄÄI J
;
ÄÄJ K
}
ÅÅ 
if
ÉÉ 
(
ÉÉ 
tipoInmueble
ÉÉ  
==
ÉÉ! #
null
ÉÉ$ (
||
ÉÉ) +
tipoInmueble
ÉÉ, 8
.
ÉÉ8 9
IdTipo
ÉÉ9 ?
<=
ÉÉ@ B
$num
ÉÉC D
)
ÉÉD E
{
ÑÑ 
return
ÖÖ 

StatusCode
ÖÖ %
(
ÖÖ% &
$num
ÖÖ& )
,
ÖÖ) *
$str
ÖÖ+ c
)
ÖÖc d
;
ÖÖd e
}
ÜÜ 
var
àà 
exist
àà 
=
àà 
await
àà !%
_tipoinmueblerepository
àà" 9
.
àà9 :!
GetTipoInmuebleById
àà: M
(
ààM N
tipoInmueble
ââ  
.
ââ  !
IdTipo
ââ! '
)
ââ' (
;
ââ( )
if
ãã 
(
ãã 
exist
ãã 
==
ãã 
null
ãã !
)
ãã! "
{
åå 
return
çç 

StatusCode
çç %
(
çç% &
$num
éé 
,
éé 
$"
éé 
$str
éé V
{
ééV W
tipoInmueble
ééW c
.
ééc d
IdTipo
ééd j
}
ééj k
$str
éék v
"
éév w
)
èè 
;
èè 
}
êê 
exist
íí 
.
íí 
Nombre
íí 
=
íí 
tipoInmueble
íí +
.
íí+ ,
Nombre
íí, 2
;
íí2 3
var
îî 
response
îî 
=
îî 
await
îî $%
_tipoinmueblerepository
îî% <
.
îî< =
PutTipoInmueble
îî= L
(
îîL M
exist
îîM R
)
îîR S
;
îîS T
return
ññ 

StatusCode
ññ !
(
ññ! "
$num
ññ" %
,
ññ% &
response
ññ' /
)
ññ/ 0
;
ññ0 1
}
óó 
catch
òò 
(
òò 
	Exception
òò 
)
òò 
{
ôô 
return
öö 

StatusCode
öö !
(
öö! "
$num
öö" %
,
öö% &
$str
öö' I
)
ööI J
;
ööJ K
}
õõ 
}
úú 	
[
ùù 	

HttpDelete
ùù	 
(
ùù 
$str
ùù 
)
ùù 
]
ùù 
[
ûû 	"
ProducesResponseType
ûû	 
(
ûû 
StatusCodes
ûû )
.
ûû) *
Status200OK
ûû* 5
)
ûû5 6
]
ûû6 7
[
üü 	"
ProducesResponseType
üü	 
(
üü 
StatusCodes
üü )
.
üü) *!
Status400BadRequest
üü* =
)
üü= >
]
üü> ?
[
†† 	"
ProducesResponseType
††	 
(
†† 
StatusCodes
†† )
.
††) *#
Status401Unauthorized
††* ?
)
††? @
]
††@ A
[
°° 	"
ProducesResponseType
°°	 
(
°° 
StatusCodes
°° )
.
°°) *
Status404NotFound
°°* ;
)
°°; <
]
°°< =
[
¢¢ 	"
ProducesResponseType
¢¢	 
(
¢¢ 
StatusCodes
¢¢ )
.
¢¢) **
Status500InternalServerError
¢¢* F
)
¢¢F G
]
¢¢G H
public
££ 
async
££ 
Task
££ 
<
££ 
IActionResult
££ '
>
££' ("
EliminarTipoInmueble
££) =
(
££= >
int
££> A
id
££B D
)
££D E
{
§§ 	
try
•• 
{
¶¶ 
if
ßß 
(
ßß 
User
ßß 
.
ßß 
Identity
ßß !
==
ßß" $
null
ßß% )
||
ßß* ,
!
ßß- .
User
ßß. 2
.
ßß2 3
Identity
ßß3 ;
.
ßß; <
IsAuthenticated
ßß< K
)
ßßK L
{
®® 
return
©© 

StatusCode
©© %
(
©©% &
$num
©©& )
,
©©) *
$str
©©+ I
)
©©I J
;
©©J K
}
™™ 
if
¨¨ 
(
¨¨ 
id
¨¨ 
<=
¨¨ 
$num
¨¨ 
)
¨¨ 
{
≠≠ 
return
ÆÆ 

StatusCode
ÆÆ %
(
ÆÆ% &
$num
ÆÆ& )
,
ÆÆ) *
$str
ÆÆ+ a
)
ÆÆa b
;
ÆÆb c
}
ØØ 
var
±± 
exist
±± 
=
±± 
await
±± !%
_tipoinmueblerepository
±±" 9
.
±±9 :!
GetTipoInmuebleById
±±: M
(
±±M N
id
±±N P
)
±±P Q
;
±±Q R
if
≤≤ 
(
≤≤ 
exist
≤≤ 
==
≤≤ 
null
≤≤ !
)
≤≤! "
{
≥≥ 
return
¥¥ 

StatusCode
¥¥ %
(
¥¥% &
$num
¥¥& )
,
¥¥) *
$"
¥¥+ -
$str
¥¥- c
{
¥¥c d
id
¥¥d f
}
¥¥f g
$str
¥¥g r
"
¥¥r s
)
¥¥s t
;
¥¥t u
}
µµ 
var
∑∑ 
response
∑∑ 
=
∑∑ 
await
∑∑ $%
_tipoinmueblerepository
∑∑% <
.
∑∑< = 
DeleteTipoInmueble
∑∑= O
(
∑∑O P
exist
∑∑P U
)
∑∑U V
;
∑∑V W
return
∏∏ 

StatusCode
∏∏ !
(
∏∏! "
$num
∏∏" %
,
∏∏% &
response
∏∏' /
)
∏∏/ 0
;
∏∏0 1
}
ππ 
catch
∫∫ 
(
∫∫ 
	Exception
∫∫ 
ex
∫∫ 
)
∫∫  
{
ªª 
return
ºº 

StatusCode
ºº !
(
ºº! "
$num
ºº" %
,
ºº% &
$"
ºº' )
$str
ºº) <
{
ºº< =
ex
ºº= ?
.
ºº? @
Message
ºº@ G
}
ººG H
"
ººH I
)
ººI J
;
ººJ K
}
ΩΩ 
}
ææ 	
}
øø 
}¿¿ ò
_C:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Controllers\rolController.cs
	namespace		 	
rever		
 
.		 
Controllers		 
{

 
[ 
Route 

(
 
$str 
) 
] 
[ 
ApiController 
] 
[ 
	Authorize 
( 
Roles 
= 
$str 
) 
] 
public 

class 
RolController 
:  
ControllerBase! /
{ 
private 
readonly 
IRolRepository '
_rolrepository( 6
;6 7
public 
RolController 
( 
IRolRepository +

repository, 6
)6 7
{ 	
_rolrepository 
= 

repository '
;' (
} 	
[ 	
HttpGet	 
] 
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *
Status200OK* 5
)5 6
]6 7
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *
Status400BadRequest* =
)= >
]> ?
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *!
Status401Unauthorized* ?
)? @
]@ A
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *
Status404NotFound* ;
); <
]< =
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *(
Status500InternalServerError* F
)F G
]G H
public 
async 
Task 
< 
IActionResult '
>' (
	ListarRol) 2
(2 3
)3 4
{ 	
try 
{   
if!! 
(!! 
User!! 
.!! 
Identity!! !
==!!" $
null!!% )
||!!* ,
!!!- .
User!!. 2
.!!2 3
Identity!!3 ;
.!!; <
IsAuthenticated!!< K
)!!K L
{"" 
return## 

StatusCode## %
(##% &
$num##& )
,##) *
$str##+ I
)##I J
;##J K
}$$ 
var&& 
response&& 
=&& 
await&& $
_rolrepository&&% 3
.&&3 4
GetRol&&4 :
(&&: ;
)&&; <
;&&< =
if(( 
((( 
response(( 
==(( 
null((  $
)(($ %
{)) 
return** 

StatusCode** %
(**% &
$num**& )
,**) *
$str**+ V
)**V W
;**W X
}++ 
return-- 

StatusCode-- !
(--! "
$num--" %
,--% &
response--' /
)--/ 0
;--0 1
}.. 
catch// 
(// 
	Exception// 
)// 
{00 
return11 

StatusCode11 !
(11! "
$num11" %
,11% &
$str11' I
)11I J
;11J K
}22 
}33 	
[55 	
HttpGet55	 
(55 
$str55 
)55 
]55 
[77 	 
ProducesResponseType77	 
(77 
StatusCodes77 )
.77) *
Status200OK77* 5
)775 6
]776 7
[88 	 
ProducesResponseType88	 
(88 
StatusCodes88 )
.88) *
Status400BadRequest88* =
)88= >
]88> ?
[99 	 
ProducesResponseType99	 
(99 
StatusCodes99 )
.99) *!
Status401Unauthorized99* ?
)99? @
]99@ A
[:: 	 
ProducesResponseType::	 
(:: 
StatusCodes:: )
.::) *
Status404NotFound::* ;
)::; <
]::< =
[;; 	 
ProducesResponseType;;	 
(;; 
StatusCodes;; )
.;;) *(
Status500InternalServerError;;* F
);;F G
];;G H
public<< 
async<< 
Task<< 
<<< 
IActionResult<< '
><<' (

ObtenerRol<<) 3
(<<3 4
int<<4 7
id<<8 :
)<<: ;
{== 	
try>> 
{?? 
if@@ 
(@@ 
User@@ 
.@@ 
Identity@@ !
==@@" $
null@@% )
||@@* ,
!@@- .
User@@. 2
.@@2 3
Identity@@3 ;
.@@; <
IsAuthenticated@@< K
)@@K L
{AA 
returnBB 

StatusCodeBB %
(BB% &
$numBB& )
,BB) *
$strBB+ I
)BBI J
;BBJ K
}CC 
ifEE 
(EE 
idEE 
<=EE 
$numEE 
)EE 
{FF 
returnGG 

StatusCodeGG %
(GG% &
$numGG& )
,GG) *
$strGG+ S
)GGS T
;GGT U
}HH 
varJJ 
existJJ 
=JJ 
awaitJJ !
_rolrepositoryJJ" 0
.JJ0 1

GetRolByIdJJ1 ;
(JJ; <
idJJ< >
)JJ> ?
;JJ? @
ifLL 
(LL 
existLL 
==LL 
nullLL !
)LL! "
{MM 
returnNN 

StatusCodeNN %
(NN% &
$numNN& )
,NN) *
$"NN+ -
$strNN- O
{NNO P
idNNP R
}NNR S
$strNNS T
"NNT U
)NNU V
;NNV W
}OO 
returnQQ 

StatusCodeQQ !
(QQ! "
$numQQ" %
,QQ% &
existQQ' ,
)QQ, -
;QQ- .
}RR 
catchSS 
(SS 
	ExceptionSS 
)SS 
{TT 
returnUU 

StatusCodeUU !
(UU! "
$numUU" %
,UU% &
$strUU' I
)UUI J
;UUJ K
}VV 
}WW 	
[YY 	
HttpPostYY	 
]YY 
[ZZ 	 
ProducesResponseTypeZZ	 
(ZZ 
StatusCodesZZ )
.ZZ) *
Status200OKZZ* 5
)ZZ5 6
]ZZ6 7
[[[ 	 
ProducesResponseType[[	 
([[ 
StatusCodes[[ )
.[[) *
Status400BadRequest[[* =
)[[= >
][[> ?
[\\ 	 
ProducesResponseType\\	 
(\\ 
StatusCodes\\ )
.\\) *!
Status401Unauthorized\\* ?
)\\? @
]\\@ A
[]] 	 
ProducesResponseType]]	 
(]] 
StatusCodes]] )
.]]) *
Status404NotFound]]* ;
)]]; <
]]]< =
[^^ 	 
ProducesResponseType^^	 
(^^ 
StatusCodes^^ )
.^^) *(
Status500InternalServerError^^* F
)^^F G
]^^G H
public__ 
async__ 
Task__ 
<__ 
IActionResult__ '
>__' (
CrearRol__) 1
(__1 2
[__2 3
FromBody__3 ;
]__; <
Rol__= @
rol__A D
)__D E
{`` 	
tryaa 
{bb 
ifcc 
(cc 
Usercc 
.cc 
Identitycc !
==cc" $
nullcc% )
||cc* ,
!cc- .
Usercc. 2
.cc2 3
Identitycc3 ;
.cc; <
IsAuthenticatedcc< K
)ccK L
{dd 
returnee 

StatusCodeee %
(ee% &
$numee& )
,ee) *
$stree+ I
)eeI J
;eeJ K
}ff 
ifhh 
(hh 
rolhh 
==hh 
nullhh 
)hh  
{ii 
returnjj 

StatusCodejj %
(jj% &
$numjj& )
,jj) *
$strjj+ X
)jjX Y
;jjY Z
}kk 
varmm 
responsemm 
=mm 
awaitmm $
_rolrepositorymm% 3
.mm3 4
PostRolmm4 ;
(mm; <
rolmm< ?
)mm? @
;mm@ A
ifoo 
(oo 
responseoo 
==oo 
nulloo  $
)oo$ %
{pp 
returnqq 

StatusCodeqq %
(qq% &
$numqq& )
,qq) *
$strqq+ ]
)qq] ^
;qq^ _
}rr 
returntt 

StatusCodett !
(tt! "
$numtt" %
,tt% &
responsett' /
)tt/ 0
;tt0 1
}uu 
catchvv 
(vv 
	Exceptionvv 
)vv 
{ww 
returnxx 

StatusCodexx !
(xx! "
$numxx" %
,xx% &
$strxx' I
)xxI J
;xxJ K
}yy 
}zz 	
[|| 	
HttpPut||	 
]|| 
[}} 	 
ProducesResponseType}}	 
(}} 
StatusCodes}} )
.}}) *
Status200OK}}* 5
)}}5 6
]}}6 7
[~~ 	 
ProducesResponseType~~	 
(~~ 
StatusCodes~~ )
.~~) *
Status400BadRequest~~* =
)~~= >
]~~> ?
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *!
Status401Unauthorized* ?
)? @
]@ A
[
ÄÄ 	"
ProducesResponseType
ÄÄ	 
(
ÄÄ 
StatusCodes
ÄÄ )
.
ÄÄ) *
Status404NotFound
ÄÄ* ;
)
ÄÄ; <
]
ÄÄ< =
[
ÅÅ 	"
ProducesResponseType
ÅÅ	 
(
ÅÅ 
StatusCodes
ÅÅ )
.
ÅÅ) **
Status500InternalServerError
ÅÅ* F
)
ÅÅF G
]
ÅÅG H
public
ÇÇ 
async
ÇÇ 
Task
ÇÇ 
<
ÇÇ 
IActionResult
ÇÇ '
>
ÇÇ' (
ActualizarRol
ÇÇ) 6
(
ÇÇ6 7
[
ÇÇ7 8
FromBody
ÇÇ8 @
]
ÇÇ@ A
Rol
ÇÇB E
rol
ÇÇF I
)
ÇÇI J
{
ÉÉ 	
try
ÑÑ 
{
ÖÖ 
if
ÜÜ 
(
ÜÜ 
User
ÜÜ 
.
ÜÜ 
Identity
ÜÜ !
==
ÜÜ" $
null
ÜÜ% )
||
ÜÜ* ,
!
ÜÜ- .
User
ÜÜ. 2
.
ÜÜ2 3
Identity
ÜÜ3 ;
.
ÜÜ; <
IsAuthenticated
ÜÜ< K
)
ÜÜK L
{
áá 
return
àà 

StatusCode
àà %
(
àà% &
$num
àà& )
,
àà) *
$str
àà+ I
)
ààI J
;
ààJ K
}
ââ 
if
ãã 
(
ãã 
rol
ãã 
==
ãã 
null
ãã 
||
ãã  "
rol
ãã# &
.
ãã& '
IdRol
ãã' ,
<=
ãã- /
$num
ãã0 1
)
ãã1 2
{
åå 
return
çç 

StatusCode
çç %
(
çç% &
$num
çç& )
,
çç) *
$str
çç+ c
)
ççc d
;
ççd e
}
éé 
var
êê 
exist
êê 
=
êê 
await
êê !
_rolrepository
êê" 0
.
êê0 1

GetRolById
êê1 ;
(
êê; <
rol
êê< ?
.
êê? @
IdRol
êê@ E
)
êêE F
;
êêF G
if
íí 
(
íí 
exist
íí 
==
íí 
null
íí !
)
íí! "
{
ìì 
return
îî 

StatusCode
îî %
(
îî% &
$num
îî& )
,
îî) *
$"
îî+ -
$str
îî- X
{
îîX Y
rol
îîY \
.
îî\ ]
IdRol
îî] b
}
îîb c
$str
îîc n
"
îîn o
)
îîo p
;
îîp q
}
ïï 
exist
óó 
.
óó 
Nombre
óó 
=
óó 
rol
óó "
.
óó" #
Nombre
óó# )
;
óó) *
var
òò 
response
òò 
=
òò 
await
òò $
_rolrepository
òò% 3
.
òò3 4
PutRol
òò4 :
(
òò: ;
exist
òò; @
)
òò@ A
;
òòA B
return
öö 

StatusCode
öö !
(
öö! "
$num
öö" %
,
öö% &
response
öö' /
)
öö/ 0
;
öö0 1
}
õõ 
catch
úú 
(
úú 
	Exception
úú 
ex
úú 
)
úú  
{
ùù 
return
ûû 

StatusCode
ûû !
(
ûû! "
$num
ûû" %
,
ûû% &
$"
ûû' )
$str
ûû) S
{
ûûS T
ex
ûûT V
.
ûûV W
Message
ûûW ^
}
ûû^ _
"
ûû_ `
)
ûû` a
;
ûûa b
}
üü 
}
†† 	
[
¢¢ 	

HttpDelete
¢¢	 
(
¢¢ 
$str
¢¢ 
)
¢¢ 
]
¢¢ 
[
££ 	"
ProducesResponseType
££	 
(
££ 
StatusCodes
££ )
.
££) *
Status200OK
££* 5
)
££5 6
]
££6 7
[
§§ 	"
ProducesResponseType
§§	 
(
§§ 
StatusCodes
§§ )
.
§§) *!
Status400BadRequest
§§* =
)
§§= >
]
§§> ?
[
•• 	"
ProducesResponseType
••	 
(
•• 
StatusCodes
•• )
.
••) *#
Status401Unauthorized
••* ?
)
••? @
]
••@ A
[
¶¶ 	"
ProducesResponseType
¶¶	 
(
¶¶ 
StatusCodes
¶¶ )
.
¶¶) *
Status404NotFound
¶¶* ;
)
¶¶; <
]
¶¶< =
[
ßß 	"
ProducesResponseType
ßß	 
(
ßß 
StatusCodes
ßß )
.
ßß) **
Status500InternalServerError
ßß* F
)
ßßF G
]
ßßG H
public
®® 
async
®® 
Task
®® 
<
®® 
IActionResult
®® '
>
®®' (
EliminarRol
®®) 4
(
®®4 5
int
®®5 8
id
®®9 ;
)
®®; <
{
©© 	
try
™™ 
{
´´ 
if
¨¨ 
(
¨¨ 
User
¨¨ 
.
¨¨ 
Identity
¨¨ !
==
¨¨" $
null
¨¨% )
||
¨¨* ,
!
¨¨- .
User
¨¨. 2
.
¨¨2 3
Identity
¨¨3 ;
.
¨¨; <
IsAuthenticated
¨¨< K
)
¨¨K L
{
≠≠ 
return
ÆÆ 

StatusCode
ÆÆ %
(
ÆÆ% &
$num
ÆÆ& )
,
ÆÆ) *
$str
ÆÆ+ I
)
ÆÆI J
;
ÆÆJ K
}
ØØ 
if
±± 
(
±± 
id
±± 
<=
±± 
$num
±± 
)
±± 
{
≤≤ 
return
≥≥ 

StatusCode
≥≥ %
(
≥≥% &
$num
≥≥& )
,
≥≥) *
$str
≥≥+ a
)
≥≥a b
;
≥≥b c
}
¥¥ 
var
∂∂ 
exist
∂∂ 
=
∂∂ 
await
∂∂ !
_rolrepository
∂∂" 0
.
∂∂0 1

GetRolById
∂∂1 ;
(
∂∂; <
id
∂∂< >
)
∂∂> ?
;
∂∂? @
if
∏∏ 
(
∏∏ 
exist
∏∏ 
==
∏∏ 
null
∏∏ !
)
∏∏! "
{
ππ 
return
∫∫ 

StatusCode
∫∫ %
(
∫∫% &
$num
ªª 
,
ªª 
$"
ºº 
$str
ºº C
{
ººC D
id
ººD F
}
ººF G
$str
ººG R
"
ººR S
)
ΩΩ 
;
ΩΩ 
}
ææ 
var
¿¿ 
response
¿¿ 
=
¿¿ 
await
¿¿ $
_rolrepository
¿¿% 3
.
¿¿3 4
	DeleteRol
¿¿4 =
(
¿¿= >
exist
¿¿> C
)
¿¿C D
;
¿¿D E
return
¬¬ 

StatusCode
¬¬ !
(
¬¬! "
$num
¬¬" %
,
¬¬% &
response
¬¬' /
)
¬¬/ 0
;
¬¬0 1
}
√√ 
catch
ƒƒ 
(
ƒƒ 
	Exception
ƒƒ 
)
ƒƒ 
{
≈≈ 
return
∆∆ 

StatusCode
∆∆ !
(
∆∆! "
$num
∆∆" %
,
∆∆% &
$str
∆∆' I
)
∆∆I J
;
∆∆J K
}
«« 
}
»» 	
}
…… 
}   ì(
kC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Controllers\ModoTransaccionController.cs
	namespace

 	
rever


 
.

 
Controllers

 
{ 
[ 
Route 

(
 
$str 
) 
] 
[ 
ApiController 
] 
[ 
	Authorize 
( 
Roles 
= 
$str 
) 
] 
public 

class %
ModoTransaccionController *
:+ ,
ControllerBase- ;
{ 
private 
readonly &
IModoTransaccionRepository 3&
_modotransaccionrepository4 N
;N O
public %
ModoTransaccionController (
(( )&
IModoTransaccionRepository) C

repositoryD N
)N O
{ 	&
_modotransaccionrepository &
=' (

repository) 3
;3 4
} 	
[ 	
HttpGet	 
] 
[ 	
AllowAnonymous	 
] 
public 
async 
Task 
< 
IActionResult '
>' (
GetAll) /
(/ 0
)0 1
{ 	
var 
modos 
= 
await &
_modotransaccionrepository 8
.8 9
GetAllAsync9 D
(D E
)E F
;F G
return 
Ok 
( 
modos 
) 
; 
} 	
[   	
HttpGet  	 
(   
$str   
)   
]   
[!! 	
AllowAnonymous!!	 
]!! 
public"" 
async"" 
Task"" 
<"" 
IActionResult"" '
>""' (
GetById"") 0
(""0 1
int""1 4
id""5 7
)""7 8
{## 	
var$$ 
modo$$ 
=$$ 
await$$ &
_modotransaccionrepository$$ 7
.$$7 8
GetByIdAsync$$8 D
($$D E
id$$E G
)$$G H
;$$H I
if%% 
(%% 
modo%% 
==%% 
null%% 
)%% 
return%% $
NotFound%%% -
(%%- .
)%%. /
;%%/ 0
return&& 
Ok&& 
(&& 
modo&& 
)&& 
;&& 
}'' 	
[)) 	
HttpPost))	 
])) 
public** 
async** 
Task** 
<** 
IActionResult** '
>**' (
Create**) /
(**/ 0
ModoTransaccion**0 ?
modo**@ D
)**D E
{++ 	
var,, 
creado,, 
=,, 
await,, &
_modotransaccionrepository,, 9
.,,9 :
CreateAsync,,: E
(,,E F
modo,,F J
),,J K
;,,K L
return-- 
CreatedAtAction-- "
(--" #
nameof--# )
(--) *
GetById--* 1
)--1 2
,--2 3
new--4 7
{--8 9
id--: <
=--= >
creado--? E
.--E F
IdModo--F L
}--M N
,--N O
creado--P V
)--V W
;--W X
}.. 	
[00 	
HttpPut00	 
(00 
$str00 
)00 
]00 
public11 
async11 
Task11 
<11 
IActionResult11 '
>11' (
Update11) /
(11/ 0
int110 3
id114 6
,116 7
ModoTransaccion118 G
modo11H L
)11L M
{22 	
if33 
(33 
id33 
!=33 
modo33 
.33 
IdModo33 !
)33! "
return33# )

BadRequest33* 4
(334 5
)335 6
;336 7
var44 
actualizado44 
=44 
await44 #&
_modotransaccionrepository44$ >
.44> ?
UpdateAsync44? J
(44J K
modo44K O
)44O P
;44P Q
return55 
Ok55 
(55 
actualizado55 !
)55! "
;55" #
}66 	
[88 	

HttpDelete88	 
(88 
$str88 
)88 
]88 
public99 
async99 
Task99 
<99 
IActionResult99 '
>99' (
Delete99) /
(99/ 0
int990 3
id994 6
)996 7
{:: 	
var;; 
	eliminado;; 
=;; 
await;; !&
_modotransaccionrepository;;" <
.;;< =
DeleteAsync;;= H
(;;H I
id;;I K
);;K L
;;;L M
if<< 
(<< 
!<< 
	eliminado<< 
)<< 
return<< "
NotFound<<# +
(<<+ ,
)<<, -
;<<- .
return== 
	NoContent== 
(== 
)== 
;== 
}>> 	
}?? 
}@@ Œ{
eC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Controllers\LocalidadController.cs
	namespace

 	
rever


 
.

 
Controllers

 
{ 
[ 
Route 

(
 
$str 
) 
] 
[ 
ApiController 
] 
[ 
	Authorize 
( 
Roles 
= 
$str 
) 
] 
public 

class 
LocalidadController $
:% &
ControllerBase' 5
{ 
private 
readonly  
ILocalidadRepository - 
_localidadrepository. B
;B C
public 
LocalidadController "
(" # 
ILocalidadRepository# 7

repository8 B
)B C
{ 	 
_localidadrepository  
=! "

repository# -
;- .
} 	
[ 	
HttpGet	 
] 
[ 	
AllowAnonymous	 
] 
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *
Status200OK* 5
)5 6
]6 7
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *!
Status401Unauthorized* ?
)? @
]@ A
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *
Status404NotFound* ;
); <
]< =
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *(
Status500InternalServerError* F
)F G
]G H
public 
async 
Task 
< 
IActionResult '
>' (
ListarLocalidad) 8
(8 9
)9 :
{ 	
try   
{!! 
var"" 
response"" 
="" 
await"" $ 
_localidadrepository""% 9
.""9 :
GetLocalidad"": F
(""F G
)""G H
;""H I
if## 
(## 
response## 
==## 
null##  $
)##$ %
{$$ 
return%% 

StatusCode%% %
(%%% &
$num%%& )
,%%) *
$str%%+ \
)%%\ ]
;%%] ^
}&& 
return'' 

StatusCode'' !
(''! "
$num''" %
,''% &
response''' /
)''/ 0
;''0 1
}(( 
catch)) 
()) 
	Exception)) 
ex)) 
)))  
{** 
return++ 

StatusCode++ !
(++! "
$num++" %
,++% &
$"++' )
$str++) <
{++< =
ex++= ?
.++? @
Message++@ G
}++G H
"++H I
)++I J
;++J K
},, 
}-- 	
[// 	
HttpGet//	 
(// 
$str// 
)// 
]// 
[00 	
AllowAnonymous00	 
]00 
[11 	 
ProducesResponseType11	 
(11 
StatusCodes11 )
.11) *
Status200OK11* 5
)115 6
]116 7
[22 	 
ProducesResponseType22	 
(22 
StatusCodes22 )
.22) *
Status400BadRequest22* =
)22= >
]22> ?
[33 	 
ProducesResponseType33	 
(33 
StatusCodes33 )
.33) *!
Status401Unauthorized33* ?
)33? @
]33@ A
[44 	 
ProducesResponseType44	 
(44 
StatusCodes44 )
.44) *
Status404NotFound44* ;
)44; <
]44< =
[55 	 
ProducesResponseType55	 
(55 
StatusCodes55 )
.55) *(
Status500InternalServerError55* F
)55F G
]55G H
public66 
async66 
Task66 
<66 
IActionResult66 '
>66' (
ObtenerLocalidad66) 9
(669 :
int66: =
id66> @
)66@ A
{77 	
try88 
{99 
if:: 
(:: 
id:: 
<=:: 
$num:: 
):: 
{;; 
return<< 

StatusCode<< %
(<<% &
$num<<& )
,<<) *
$str<<+ S
)<<S T
;<<T U
}== 
var?? 
response?? 
=?? 
await?? $ 
_localidadrepository??% 9
.??9 :
GetLocalidadById??: J
(??J K
id??K M
)??M N
;??N O
if@@ 
(@@ 
response@@ 
==@@ 
null@@  $
)@@$ %
{AA 
returnBB 

StatusCodeBB %
(BB% &
$numBB& )
,BB) *
$"BB+ -
$strBB- U
{BBU V
idBBV X
}BBX Y
$strBBY Z
"BBZ [
)BB[ \
;BB\ ]
}CC 
returnDD 

StatusCodeDD !
(DD! "
$numDD" %
,DD% &
responseDD' /
)DD/ 0
;DD0 1
}EE 
catchFF 
(FF 
	ExceptionFF 
exFF 
)FF  
{GG 
returnHH 

StatusCodeHH !
(HH! "
$numHH" %
,HH% &
$"HH' )
$strHH) <
{HH< =
exHH= ?
.HH? @
MessageHH@ G
}HHG H
"HHH I
)HHI J
;HHJ K
}II 
}JJ 	
[LL 	
HttpPostLL	 
]LL 
[MM 	 
ProducesResponseTypeMM	 
(MM 
StatusCodesMM )
.MM) *
Status200OKMM* 5
)MM5 6
]MM6 7
[NN 	 
ProducesResponseTypeNN	 
(NN 
StatusCodesNN )
.NN) *
Status400BadRequestNN* =
)NN= >
]NN> ?
[OO 	 
ProducesResponseTypeOO	 
(OO 
StatusCodesOO )
.OO) *!
Status401UnauthorizedOO* ?
)OO? @
]OO@ A
[PP 	 
ProducesResponseTypePP	 
(PP 
StatusCodesPP )
.PP) *(
Status500InternalServerErrorPP* F
)PPF G
]PPG H
publicQQ 
asyncQQ 
TaskQQ 
<QQ 
IActionResultQQ '
>QQ' (
CrearLocalidadQQ) 7
(QQ7 8
[QQ8 9
FromBodyQQ9 A
]QQA B
	LocalidadQQC L
	localidadQQM V
)QQV W
{RR 	
trySS 
{TT 
ifUU 
(UU 
UserUU 
.UU 
IdentityUU !
==UU" $
nullUU% )
||UU* ,
!UU- .
UserUU. 2
.UU2 3
IdentityUU3 ;
.UU; <
IsAuthenticatedUU< K
)UUK L
{VV 
returnWW 

StatusCodeWW %
(WW% &
$numWW& )
,WW) *
$strWW+ I
)WWI J
;WWJ K
}XX 
ifZZ 
(ZZ 
	localidadZZ 
==ZZ  
nullZZ! %
)ZZ% &
{[[ 
return\\ 

StatusCode\\ %
(\\% &
$num\\& )
,\\) *
$str\\+ `
)\\` a
;\\a b
}]] 
var__ 
response__ 
=__ 
await__ $ 
_localidadrepository__% 9
.__9 :
PostLocalidad__: G
(__G H
	localidad__H Q
)__Q R
;__R S
if`` 
(`` 
response`` 
==`` 
null``  $
)``$ %
{aa 
returnbb 

StatusCodebb %
(bb% &
$numbb& )
,bb) *
$strbb+ ]
)bb] ^
;bb^ _
}cc 
returndd 

StatusCodedd !
(dd! "
$numdd" %
,dd% &
responsedd' /
)dd/ 0
;dd0 1
}ee 
catchff 
(ff 
	Exceptionff 
exff 
)ff  
{gg 
returnhh 

StatusCodehh !
(hh! "
$numhh" %
,hh% &
$"hh' )
$strhh) <
{hh< =
exhh= ?
.hh? @
Messagehh@ G
}hhG H
"hhH I
)hhI J
;hhJ K
}ii 
}jj 	
[ll 	
HttpPutll	 
]ll 
[mm 	 
ProducesResponseTypemm	 
(mm 
StatusCodesmm )
.mm) *
Status200OKmm* 5
)mm5 6
]mm6 7
[nn 	 
ProducesResponseTypenn	 
(nn 
StatusCodesnn )
.nn) *
Status400BadRequestnn* =
)nn= >
]nn> ?
[oo 	 
ProducesResponseTypeoo	 
(oo 
StatusCodesoo )
.oo) *!
Status401Unauthorizedoo* ?
)oo? @
]oo@ A
[pp 	 
ProducesResponseTypepp	 
(pp 
StatusCodespp )
.pp) *
Status404NotFoundpp* ;
)pp; <
]pp< =
[qq 	 
ProducesResponseTypeqq	 
(qq 
StatusCodesqq )
.qq) *(
Status500InternalServerErrorqq* F
)qqF G
]qqG H
publicrr 
asyncrr 
Taskrr 
<rr 
IActionResultrr '
>rr' (
ActualizarLocalidadrr) <
(rr< =
[rr= >
FromBodyrr> F
]rrF G
	LocalidadrrH Q
	localidadrrR [
)rr[ \
{ss 	
trytt 
{uu 
ifvv 
(vv 
Uservv 
.vv 
Identityvv !
==vv" $
nullvv% )
||vv* ,
!vv- .
Uservv. 2
.vv2 3
Identityvv3 ;
.vv; <
IsAuthenticatedvv< K
)vvK L
{ww 
returnxx 

StatusCodexx %
(xx% &
$numxx& )
,xx) *
$strxx+ I
)xxI J
;xxJ K
}yy 
if{{ 
({{ 
	localidad{{ 
=={{  
null{{! %
||{{& (
	localidad{{) 2
.{{2 3
IdLocalidad{{3 >
<={{? A
$num{{B C
){{C D
{|| 
return}} 

StatusCode}} %
(}}% &
$num}}& )
,}}) *
$str}}+ c
)}}c d
;}}d e
}~~ 
var
ÄÄ 
exist
ÄÄ 
=
ÄÄ 
await
ÄÄ !"
_localidadrepository
ÄÄ" 6
.
ÄÄ6 7
GetLocalidadById
ÄÄ7 G
(
ÄÄG H
	localidad
ÄÄH Q
.
ÄÄQ R
IdLocalidad
ÄÄR ]
)
ÄÄ] ^
;
ÄÄ^ _
if
ÅÅ 
(
ÅÅ 
exist
ÅÅ 
==
ÅÅ 
null
ÅÅ !
)
ÅÅ! "
{
ÇÇ 
return
ÉÉ 

StatusCode
ÉÉ %
(
ÉÉ% &
$num
ÉÉ& )
,
ÉÉ) *
$"
ÉÉ+ -
$str
ÉÉ- ^
{
ÉÉ^ _
	localidad
ÉÉ_ h
.
ÉÉh i
IdLocalidad
ÉÉi t
}
ÉÉt u
$strÉÉu Ä
"ÉÉÄ Å
)ÉÉÅ Ç
;ÉÉÇ É
}
ÑÑ 
exist
ÜÜ 
.
ÜÜ 
Nombre
ÜÜ 
=
ÜÜ 
	localidad
ÜÜ (
.
ÜÜ( )
Nombre
ÜÜ) /
;
ÜÜ/ 0
var
àà 
response
àà 
=
àà 
await
àà $"
_localidadrepository
àà% 9
.
àà9 :
PutLocalidad
àà: F
(
ààF G
exist
ààG L
)
ààL M
;
ààM N
return
ââ 

StatusCode
ââ !
(
ââ! "
$num
ââ" %
,
ââ% &
response
ââ' /
)
ââ/ 0
;
ââ0 1
}
ää 
catch
ãã 
(
ãã 
	Exception
ãã 
ex
ãã 
)
ãã  
{
åå 
return
çç 

StatusCode
çç !
(
çç! "
$num
çç" %
,
çç% &
$"
çç' )
$str
çç) <
{
çç< =
ex
çç= ?
.
çç? @
Message
çç@ G
}
ççG H
"
ççH I
)
ççI J
;
ççJ K
}
éé 
}
èè 	
[
ëë 	

HttpDelete
ëë	 
(
ëë 
$str
ëë 
)
ëë 
]
ëë 
[
íí 	"
ProducesResponseType
íí	 
(
íí 
StatusCodes
íí )
.
íí) *
Status200OK
íí* 5
)
íí5 6
]
íí6 7
[
ìì 	"
ProducesResponseType
ìì	 
(
ìì 
StatusCodes
ìì )
.
ìì) *!
Status400BadRequest
ìì* =
)
ìì= >
]
ìì> ?
[
îî 	"
ProducesResponseType
îî	 
(
îî 
StatusCodes
îî )
.
îî) *#
Status401Unauthorized
îî* ?
)
îî? @
]
îî@ A
[
ïï 	"
ProducesResponseType
ïï	 
(
ïï 
StatusCodes
ïï )
.
ïï) *
Status404NotFound
ïï* ;
)
ïï; <
]
ïï< =
[
ññ 	"
ProducesResponseType
ññ	 
(
ññ 
StatusCodes
ññ )
.
ññ) **
Status500InternalServerError
ññ* F
)
ññF G
]
ññG H
public
óó 
async
óó 
Task
óó 
<
óó 
IActionResult
óó '
>
óó' (
EliminarLocalidad
óó) :
(
óó: ;
int
óó; >
id
óó? A
)
óóA B
{
òò 	
try
ôô 
{
öö 
if
õõ 
(
õõ 
User
õõ 
.
õõ 
Identity
õõ !
==
õõ" $
null
õõ% )
||
õõ* ,
!
õõ- .
User
õõ. 2
.
õõ2 3
Identity
õõ3 ;
.
õõ; <
IsAuthenticated
õõ< K
)
õõK L
{
úú 
return
ùù 

StatusCode
ùù %
(
ùù% &
$num
ùù& )
,
ùù) *
$str
ùù+ I
)
ùùI J
;
ùùJ K
}
ûû 
if
†† 
(
†† 
id
†† 
<=
†† 
$num
†† 
)
†† 
{
°° 
return
¢¢ 

StatusCode
¢¢ %
(
¢¢% &
$num
¢¢& )
,
¢¢) *
$str
¢¢+ a
)
¢¢a b
;
¢¢b c
}
££ 
var
•• 
exist
•• 
=
•• 
await
•• !"
_localidadrepository
••" 6
.
••6 7
GetLocalidadById
••7 G
(
••G H
id
••H J
)
••J K
;
••K L
if
¶¶ 
(
¶¶ 
exist
¶¶ 
==
¶¶ 
null
¶¶ !
)
¶¶! "
{
ßß 
return
®® 

StatusCode
®® %
(
®®% &
$num
®®& )
,
®®) *
$"
®®+ -
$str
®®- \
{
®®\ ]
id
®®] _
}
®®_ `
$str
®®` k
"
®®k l
)
®®l m
;
®®m n
}
©© 
var
´´ 
response
´´ 
=
´´ 
await
´´ $"
_localidadrepository
´´% 9
.
´´9 :
DeleteLocalidad
´´: I
(
´´I J
exist
´´J O
)
´´O P
;
´´P Q
return
¨¨ 

StatusCode
¨¨ !
(
¨¨! "
$num
¨¨" %
,
¨¨% &
response
¨¨' /
)
¨¨/ 0
;
¨¨0 1
}
≠≠ 
catch
ÆÆ 
(
ÆÆ 
	Exception
ÆÆ 
ex
ÆÆ 
)
ÆÆ  
{
ØØ 
return
∞∞ 

StatusCode
∞∞ !
(
∞∞! "
$num
∞∞" %
,
∞∞% &
$"
∞∞' )
$str
∞∞) <
{
∞∞< =
ex
∞∞= ?
.
∞∞? @
Message
∞∞@ G
}
∞∞G H
"
∞∞H I
)
∞∞I J
;
∞∞J K
}
±± 
}
≤≤ 	
}
≥≥ 
}¥¥ ì∆
dC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Controllers\InmuebleController.cs
	namespace 	
rever
 
. 
Controllers 
{ 
[ 
Route 

(
 
$str 
) 
] 
[ 
ApiController 
] 
[ 
	Authorize 
( 
Roles 
= 
$str 
) 
] 
public 

class 
InmuebleController #
:$ %
ControllerBase& 4
{ 
private 
readonly 
IInmuebleRepository ,
_inmueblerepository- @
;@ A
public 
InmuebleController !
(! "
IInmuebleRepository" 5

repository6 @
)@ A
{ 	
_inmueblerepository 
=  !

repository" ,
;, -
} 	
[ 	
HttpGet	 
( 
$str 
) 
] 
[ 	
AllowAnonymous	 
] 
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *
Status200OK* 5
)5 6
]6 7
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *
Status404NotFound* ;
); <
]< =
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *(
Status500InternalServerError* F
)F G
]G H
public   
async   
Task   
<   
IActionResult   '
>  ' (
ListarInmueble  ) 7
(  7 8
)  8 9
{!! 	
try"" 
{## 
var$$ 
response$$ 
=$$ 
await$$ $
_inmueblerepository$$% 8
.$$8 9
GetInmueble$$9 D
($$D E
)$$E F
;$$F G
if%% 
(%% 
response%% 
==%% 
null%%  $
)%%$ %
{&& 
return'' 

StatusCode'' %
(''% &
$num''& )
,'') *
$str''+ Z
)''Z [
;''[ \
}(( 
return)) 

StatusCode)) !
())! "
$num))" %
,))% &
response))' /
)))/ 0
;))0 1
}** 
catch++ 
(++ 
	Exception++ 
ex++ 
)++  
{,, 
return-- 

StatusCode-- !
(--! "
$num--" %
,--% &
$"--' )
$str--) <
{--< =
ex--= ?
.--? @
Message--@ G
}--G H
"--H I
)--I J
;--J K
}.. 
}// 	
[11 	
HttpGet11	 
(11 
$str11 
)11 
]11 
[22 	
AllowAnonymous22	 
]22 
[33 	 
ProducesResponseType33	 
(33 
StatusCodes33 )
.33) *
Status200OK33* 5
)335 6
]336 7
[44 	 
ProducesResponseType44	 
(44 
StatusCodes44 )
.44) *
Status400BadRequest44* =
)44= >
]44> ?
[55 	 
ProducesResponseType55	 
(55 
StatusCodes55 )
.55) *
Status404NotFound55* ;
)55; <
]55< =
[66 	 
ProducesResponseType66	 
(66 
StatusCodes66 )
.66) *(
Status500InternalServerError66* F
)66F G
]66G H
public77 
async77 
Task77 
<77 
IActionResult77 '
>77' (
ObtenerInmueble77) 8
(778 9
int779 <
id77= ?
)77? @
{88 	
try99 
{:: 
if;; 
(;; 
id;; 
<=;; 
$num;; 
);; 
{<< 
return== 

StatusCode== %
(==% &
$num==& )
,==) *
$str==+ S
)==S T
;==T U
}>> 
var@@ 
response@@ 
=@@ 
await@@ $
_inmueblerepository@@% 8
.@@8 9
GetInmuebleById@@9 H
(@@H I
id@@I K
)@@K L
;@@L M
ifAA 
(AA 
responseAA 
==AA 
nullAA  $
)AA$ %
{BB 
returnCC 

StatusCodeCC %
(CC% &
$numCC& )
,CC) *
$"CC+ -
$strCC- T
{CCT U
idCCU W
}CCW X
$strCCX Y
"CCY Z
)CCZ [
;CC[ \
}DD 
returnEE 

StatusCodeEE !
(EE! "
$numEE" %
,EE% &
responseEE' /
)EE/ 0
;EE0 1
}FF 
catchGG 
(GG 
	ExceptionGG 
exGG 
)GG  
{HH 
returnII 

StatusCodeII !
(II! "
$numII" %
,II% &
$"II' )
$strII) <
{II< =
exII= ?
.II? @
MessageII@ G
}IIG H
"IIH I
)III J
;IIJ K
}JJ 
}KK 	
[MM 	
HttpGetMM	 
(MM 
$strMM 
)MM  
]MM  !
[NN 	
AllowAnonymousNN	 
]NN 
publicOO 
asyncOO 
TaskOO 
<OO 
IActionResultOO '
>OO' ("
ObtenerDetalleInmuebleOO) ?
(OO? @
intOO@ C
idOOD F
)OOF G
{PP 	
ifQQ 
(QQ 
idQQ 
<=QQ 
$numQQ 
)QQ 
{RR 
returnSS 

StatusCodeSS !
(SS! "
$numSS" %
,SS% &
$strSS' O
)SSO P
;SSP Q
}TT 
varVV 
detalleVV 
=VV 
awaitVV 
_inmueblerepositoryVV  3
.VV3 4
GetDetalleAsyncVV4 C
(VVC D
idVVD F
)VVF G
;VVG H
ifWW 
(WW 
detalleWW 
==WW 
nullWW 
)WW  
{XX 
returnYY 

StatusCodeYY !
(YY! "
$numYY" %
,YY% &
$"YY' )
$strYY) P
{YYP Q
idYYQ S
}YYS T
$strYYT U
"YYU V
)YYV W
;YYW X
}ZZ 
return\\ 
Ok\\ 
(\\ 
detalle\\ 
)\\ 
;\\ 
}]] 	
[__ 	
HttpPost__	 
]__ 
[`` 	 
ProducesResponseType``	 
(`` 
StatusCodes`` )
.``) *
Status200OK``* 5
)``5 6
]``6 7
[aa 	 
ProducesResponseTypeaa	 
(aa 
StatusCodesaa )
.aa) *
Status400BadRequestaa* =
)aa= >
]aa> ?
[bb 	 
ProducesResponseTypebb	 
(bb 
StatusCodesbb )
.bb) *!
Status401Unauthorizedbb* ?
)bb? @
]bb@ A
[cc 	 
ProducesResponseTypecc	 
(cc 
StatusCodescc )
.cc) *(
Status500InternalServerErrorcc* F
)ccF G
]ccG H
publicdd 
asyncdd 
Taskdd 
<dd 
IActionResultdd '
>dd' (
CrearInmuebledd) 6
(dd6 7
[dd7 8
FromBodydd8 @
]dd@ A
InmuebleddB J
inmuebleddK S
)ddS T
{ee 	
tryff 
{gg 
ifhh 
(hh 
inmueblehh 
==hh 
nullhh  $
)hh$ %
{ii 
returnjj 

StatusCodejj %
(jj% &
$numjj& )
,jj) *
$strjj+ ]
)jj] ^
;jj^ _
}kk 
varnn 
idUsuarioTokennn "
=nn# $!
ObtenerIdUsuarioTokennn% :
(nn: ;
)nn; <
;nn< =
ifpp 
(pp 
!pp 
idUsuarioTokenpp #
.pp# $
HasValuepp$ ,
)pp, -
{qq 
returnrr 

StatusCoderr %
(rr% &
$numrr& )
,rr) *
$strrr+ c
)rrc d
;rrd e
}ss 
inmueblevv 
.vv 
	IdUsuariovv "
=vv# $
idUsuarioTokenvv% 3
.vv3 4
Valuevv4 9
;vv9 :
varyy 
responseyy 
=yy 
awaityy $
_inmueblerepositoryyy% 8
.yy8 9
PostInmuebleyy9 E
(yyE F
inmuebleyyF N
)yyN O
;yyO P
if{{ 
({{ 
response{{ 
=={{ 
null{{  $
){{$ %
{|| 
return}} 

StatusCode}} %
(}}% &
$num}}& )
,}}) *
$str}}+ ]
)}}] ^
;}}^ _
}~~ 
return
ÄÄ 

StatusCode
ÄÄ !
(
ÄÄ! "
$num
ÄÄ" %
,
ÄÄ% &
response
ÄÄ' /
)
ÄÄ/ 0
;
ÄÄ0 1
}
ÅÅ 
catch
ÇÇ 
(
ÇÇ 
	Exception
ÇÇ 
ex
ÇÇ 
)
ÇÇ  
{
ÉÉ 
return
ÑÑ 

StatusCode
ÑÑ !
(
ÑÑ! "
$num
ÑÑ" %
,
ÑÑ% &
$"
ÑÑ' )
$str
ÑÑ) <
{
ÑÑ< =
ex
ÑÑ= ?
.
ÑÑ? @
Message
ÑÑ@ G
}
ÑÑG H
"
ÑÑH I
)
ÑÑI J
;
ÑÑJ K
}
ÖÖ 
}
ÜÜ 	
[
àà 	
HttpPut
àà	 
]
àà 
[
ââ 	"
ProducesResponseType
ââ	 
(
ââ 
StatusCodes
ââ )
.
ââ) *
Status200OK
ââ* 5
)
ââ5 6
]
ââ6 7
[
ää 	"
ProducesResponseType
ää	 
(
ää 
StatusCodes
ää )
.
ää) *!
Status400BadRequest
ää* =
)
ää= >
]
ää> ?
[
ãã 	"
ProducesResponseType
ãã	 
(
ãã 
StatusCodes
ãã )
.
ãã) * 
Status403Forbidden
ãã* <
)
ãã< =
]
ãã= >
[
åå 	"
ProducesResponseType
åå	 
(
åå 
StatusCodes
åå )
.
åå) *
Status404NotFound
åå* ;
)
åå; <
]
åå< =
[
çç 	"
ProducesResponseType
çç	 
(
çç 
StatusCodes
çç )
.
çç) **
Status500InternalServerError
çç* F
)
ççF G
]
ççG H
public
éé 
async
éé 
Task
éé 
<
éé 
IActionResult
éé '
>
éé' ( 
ActualizarInmueble
éé) ;
(
éé; <
[
éé< =
FromBody
éé= E
]
ééE F
Inmueble
ééG O
inmueble
ééP X
)
ééX Y
{
èè 	
try
êê 
{
ëë 
if
íí 
(
íí 
inmueble
íí 
==
íí 
null
íí  $
||
íí% '
inmueble
íí( 0
.
íí0 1

IdInmueble
íí1 ;
<=
íí< >
$num
íí? @
)
íí@ A
{
ìì 
return
îî 

StatusCode
îî %
(
îî% &
$num
îî& )
,
îî) *
$str
îî+ c
)
îîc d
;
îîd e
}
ïï 
var
óó 
exist
óó 
=
óó 
await
óó !!
_inmueblerepository
óó" 5
.
óó5 6
GetInmuebleById
óó6 E
(
óóE F
inmueble
óóF N
.
óóN O

IdInmueble
óóO Y
)
óóY Z
;
óóZ [
if
òò 
(
òò 
exist
òò 
==
òò 
null
òò !
)
òò! "
{
ôô 
return
öö 

StatusCode
öö %
(
öö% &
$num
öö& )
,
öö) *
$"
öö+ -
$str
öö- ]
{
öö] ^
inmueble
öö^ f
.
ööf g

IdInmueble
öög q
}
ööq r
$str
öör }
"
öö} ~
)
öö~ 
;öö Ä
}
õõ 
if
ùù 
(
ùù 
!
ùù 
EsDuenoOAdmin
ùù "
(
ùù" #
exist
ùù# (
.
ùù( )
	IdUsuario
ùù) 2
)
ùù2 3
)
ùù3 4
{
ûû 
return
üü 

StatusCode
üü %
(
üü% &
$num
üü& )
,
üü) *
$str
üü+ f
)
üüf g
;
üüg h
}
†† 
exist
¢¢ 
.
¢¢ 
Titulo
¢¢ 
=
¢¢ 
inmueble
¢¢ '
.
¢¢' (
Titulo
¢¢( .
;
¢¢. /
exist
££ 
.
££ 
Descripcion
££ !
=
££" #
inmueble
££$ ,
.
££, -
Descripcion
££- 8
;
££8 9
exist
§§ 
.
§§ 
Precio
§§ 
=
§§ 
inmueble
§§ '
.
§§' (
Precio
§§( .
;
§§. /
exist
•• 
.
•• 
IdTipo
•• 
=
•• 
inmueble
•• '
.
••' (
IdTipo
••( .
;
••. /
exist
¶¶ 
.
¶¶ 
IdModo
¶¶ 
=
¶¶ 
inmueble
¶¶ '
.
¶¶' (
IdModo
¶¶( .
;
¶¶. /
exist
ßß 
.
ßß 
	Direccion
ßß 
=
ßß  !
inmueble
ßß" *
.
ßß* +
	Direccion
ßß+ 4
;
ßß4 5
exist
®® 
.
®® 
IdLocalidad
®® !
=
®®" #
inmueble
®®$ ,
.
®®, -
IdLocalidad
®®- 8
;
®®8 9
exist
©© 
.
©© 
Habitaciones
©© "
=
©©# $
inmueble
©©% -
.
©©- .
Habitaciones
©©. :
;
©©: ;
exist
™™ 
.
™™ 
Ba√±os
™™ 
=
™™ 
inmueble
™™ &
.
™™& '
Ba√±os
™™' ,
;
™™, -
exist
´´ 
.
´´ 
MetrosCuadrados
´´ %
=
´´& '
inmueble
´´( 0
.
´´0 1
MetrosCuadrados
´´1 @
;
´´@ A
exist
¨¨ 
.
¨¨ 
Estrato
¨¨ 
=
¨¨ 
inmueble
¨¨  (
.
¨¨( )
Estrato
¨¨) 0
;
¨¨0 1
exist
≠≠ 
.
≠≠ 
Pisos
≠≠ 
=
≠≠ 
inmueble
≠≠ &
.
≠≠& '
Pisos
≠≠' ,
;
≠≠, -
exist
ÆÆ 
.
ÆÆ 
Latitud
ÆÆ 
=
ÆÆ 
inmueble
ÆÆ  (
.
ÆÆ( )
Latitud
ÆÆ) 0
;
ÆÆ0 1
exist
ØØ 
.
ØØ 
Longitud
ØØ 
=
ØØ  
inmueble
ØØ! )
.
ØØ) *
Longitud
ØØ* 2
;
ØØ2 3
exist
∞∞ 
.
∞∞ 
IdEstado
∞∞ 
=
∞∞  
inmueble
∞∞! )
.
∞∞) *
IdEstado
∞∞* 2
;
∞∞2 3
var
¥¥ 
response
¥¥ 
=
¥¥ 
await
¥¥ $!
_inmueblerepository
¥¥% 8
.
¥¥8 9
PutInmueble
¥¥9 D
(
¥¥D E
exist
¥¥E J
)
¥¥J K
;
¥¥K L
return
µµ 

StatusCode
µµ !
(
µµ! "
$num
µµ" %
,
µµ% &
response
µµ' /
)
µµ/ 0
;
µµ0 1
}
∂∂ 
catch
∑∑ 
(
∑∑ 
	Exception
∑∑ 
ex
∑∑ 
)
∑∑  
{
∏∏ 
return
ππ 

StatusCode
ππ !
(
ππ! "
$num
ππ" %
,
ππ% &
$"
ππ' )
$str
ππ) <
{
ππ< =
ex
ππ= ?
.
ππ? @
Message
ππ@ G
}
ππG H
"
ππH I
)
ππI J
;
ππJ K
}
∫∫ 
}
ªª 	
[
ΩΩ 	

HttpDelete
ΩΩ	 
(
ΩΩ 
$str
ΩΩ 
)
ΩΩ 
]
ΩΩ 
[
ææ 	
	Authorize
ææ	 
(
ææ 
Roles
ææ 
=
ææ 
$str
ææ 3
)
ææ3 4
]
ææ4 5
[
øø 	"
ProducesResponseType
øø	 
(
øø 
StatusCodes
øø )
.
øø) *
Status200OK
øø* 5
)
øø5 6
]
øø6 7
[
¿¿ 	"
ProducesResponseType
¿¿	 
(
¿¿ 
StatusCodes
¿¿ )
.
¿¿) *!
Status400BadRequest
¿¿* =
)
¿¿= >
]
¿¿> ?
[
¡¡ 	"
ProducesResponseType
¡¡	 
(
¡¡ 
StatusCodes
¡¡ )
.
¡¡) * 
Status403Forbidden
¡¡* <
)
¡¡< =
]
¡¡= >
[
¬¬ 	"
ProducesResponseType
¬¬	 
(
¬¬ 
StatusCodes
¬¬ )
.
¬¬) *
Status404NotFound
¬¬* ;
)
¬¬; <
]
¬¬< =
[
√√ 	"
ProducesResponseType
√√	 
(
√√ 
StatusCodes
√√ )
.
√√) **
Status500InternalServerError
√√* F
)
√√F G
]
√√G H
public
ƒƒ 
async
ƒƒ 
Task
ƒƒ 
<
ƒƒ 
IActionResult
ƒƒ '
>
ƒƒ' (
EliminarInmueble
ƒƒ) 9
(
ƒƒ9 :
int
ƒƒ: =
id
ƒƒ> @
)
ƒƒ@ A
{
≈≈ 	
try
∆∆ 
{
«« 
if
»» 
(
»» 
id
»» 
<=
»» 
$num
»» 
)
»» 
{
…… 
return
   

StatusCode
   %
(
  % &
$num
  & )
,
  ) *
$str
  + a
)
  a b
;
  b c
}
ÀÀ 
var
ÕÕ 
exist
ÕÕ 
=
ÕÕ 
await
ÕÕ !!
_inmueblerepository
ÕÕ" 5
.
ÕÕ5 6
GetInmuebleById
ÕÕ6 E
(
ÕÕE F
id
ÕÕF H
)
ÕÕH I
;
ÕÕI J
if
ŒŒ 
(
ŒŒ 
exist
ŒŒ 
==
ŒŒ 
null
ŒŒ !
)
ŒŒ! "
{
œœ 
return
–– 

StatusCode
–– %
(
––% &
$num
––& )
,
––) *
$"
––+ -
$str
––- [
{
––[ \
id
––\ ^
}
––^ _
$str
––_ j
"
––j k
)
––k l
;
––l m
}
—— 
if
”” 
(
”” 
!
”” 
EsDuenoOAdmin
”” "
(
””" #
exist
””# (
.
””( )
	IdUsuario
””) 2
)
””2 3
)
””3 4
{
‘‘ 
return
’’ 

StatusCode
’’ %
(
’’% &
$num
’’& )
,
’’) *
$str
’’+ e
)
’’e f
;
’’f g
}
÷÷ 
var
ÿÿ 
response
ÿÿ 
=
ÿÿ 
await
ÿÿ $!
_inmueblerepository
ÿÿ% 8
.
ÿÿ8 9
DeleteInmueble
ÿÿ9 G
(
ÿÿG H
exist
ÿÿH M
)
ÿÿM N
;
ÿÿN O
return
ŸŸ 

StatusCode
ŸŸ !
(
ŸŸ! "
$num
ŸŸ" %
,
ŸŸ% &
response
ŸŸ' /
)
ŸŸ/ 0
;
ŸŸ0 1
}
⁄⁄ 
catch
€€ 
(
€€ 
	Exception
€€ 
ex
€€ 
)
€€  
{
‹‹ 
return
›› 

StatusCode
›› !
(
››! "
$num
››" %
,
››% &
$"
››' )
$str
››) <
{
››< =
ex
››= ?
.
››? @
Message
››@ G
}
››G H
"
››H I
)
››I J
;
››J K
}
ﬁﬁ 
}
ﬂﬂ 	
[
·· 	
HttpGet
··	 
]
·· 
[
‚‚ 	
AllowAnonymous
‚‚	 
]
‚‚ 
public
„„ 
async
„„ 
Task
„„ 
<
„„ 
IActionResult
„„ '
>
„„' (
GetAll
„„) /
(
„„/ 0
)
„„0 1
{
‰‰ 	
var
ÂÂ 
	inmuebles
ÂÂ 
=
ÂÂ 
await
ÂÂ !!
_inmueblerepository
ÂÂ" 5
.
ÂÂ5 6
GetListadoAsync
ÂÂ6 E
(
ÂÂE F
)
ÂÂF G
;
ÂÂG H
return
ÊÊ 
Ok
ÊÊ 
(
ÊÊ 
	inmuebles
ÊÊ 
)
ÊÊ  
;
ÊÊ  !
}
ÁÁ 	
private
ÈÈ 
int
ÈÈ 
?
ÈÈ #
ObtenerIdUsuarioToken
ÈÈ *
(
ÈÈ* +
)
ÈÈ+ ,
{
ÍÍ 	
return
ÎÎ 
User
ÎÎ 
.
ÎÎ 
ObtenerIdUsuario
ÎÎ (
(
ÎÎ( )
)
ÎÎ) *
;
ÎÎ* +
}
ÏÏ 	
private
ÓÓ 
bool
ÓÓ 
EsDuenoOAdmin
ÓÓ "
(
ÓÓ" #
int
ÓÓ# &
idDuenoDelRecurso
ÓÓ' 8
)
ÓÓ8 9
{
ÔÔ 	
var
 
idUsuarioToken
 
=
  #
ObtenerIdUsuarioToken
! 6
(
6 7
)
7 8
;
8 9
if
ÚÚ 
(
ÚÚ 
!
ÚÚ 
idUsuarioToken
ÚÚ 
.
ÚÚ  
HasValue
ÚÚ  (
)
ÚÚ( )
{
ÛÛ 
return
ÙÙ 
false
ÙÙ 
;
ÙÙ 
}
ıı 
var
˜˜ 
esAdmin
˜˜ 
=
˜˜ 
User
˜˜ 
.
˜˜ 
IsInRole
˜˜ '
(
˜˜' (
$str
˜˜( 7
)
˜˜7 8
||
˜˜9 ;
User
˜˜< @
.
˜˜@ A
IsInRole
˜˜A I
(
˜˜I J
$str
˜˜J Q
)
˜˜Q R
;
˜˜R S
return
˘˘ 
idDuenoDelRecurso
˘˘ $
==
˘˘% '
idUsuarioToken
˘˘( 6
.
˘˘6 7
Value
˘˘7 <
||
˘˘= ?
esAdmin
˘˘@ G
;
˘˘G H
}
˙˙ 	
[
¸¸ 	
HttpPost
¸¸	 
(
¸¸ 
$str
¸¸ 
)
¸¸ 
]
¸¸ 
[
˝˝ 	"
ProducesResponseType
˝˝	 
(
˝˝ 
StatusCodes
˝˝ )
.
˝˝) *
Status201Created
˝˝* :
)
˝˝: ;
]
˝˝; <
[
˛˛ 	"
ProducesResponseType
˛˛	 
(
˛˛ 
StatusCodes
˛˛ )
.
˛˛) *!
Status400BadRequest
˛˛* =
)
˛˛= >
]
˛˛> ?
[
ˇˇ 	"
ProducesResponseType
ˇˇ	 
(
ˇˇ 
StatusCodes
ˇˇ )
.
ˇˇ) *#
Status401Unauthorized
ˇˇ* ?
)
ˇˇ? @
]
ˇˇ@ A
[
ÄÄ 	"
ProducesResponseType
ÄÄ	 
(
ÄÄ 
StatusCodes
ÄÄ )
.
ÄÄ) **
Status500InternalServerError
ÄÄ* F
)
ÄÄF G
]
ÄÄG H
public
ÅÅ 
async
ÅÅ 
Task
ÅÅ 
<
ÅÅ 
IActionResult
ÅÅ '
>
ÅÅ' (#
CrearInmuebleCompleto
ÅÅ) >
(
ÅÅ> ?
[
ÅÅ? @
FromBody
ÅÅ@ H
]
ÅÅH I
CrearInmuebleDto
ÅÅJ Z
dto
ÅÅ[ ^
)
ÅÅ^ _
{
ÇÇ 	
try
ÉÉ 
{
ÑÑ 
var
áá 
idUsuarioToken
áá "
=
áá# $#
ObtenerIdUsuarioToken
áá% :
(
áá: ;
)
áá; <
;
áá< =
if
àà 
(
àà 
!
àà 
idUsuarioToken
àà #
.
àà# $
HasValue
àà$ ,
)
àà, -
{
ââ 
return
ää 

StatusCode
ää %
(
ää% &
$num
ää& )
,
ää) *
$str
ää+ c
)
ääc d
;
ääd e
}
ãã 
var
çç 
response
çç 
=
çç 
await
çç $!
_inmueblerepository
çç% 8
.
çç8 9"
PostInmuebleCompleto
çç9 M
(
ççM N
dto
ççN Q
,
ççQ R
idUsuarioToken
ççS a
.
çça b
Value
ççb g
)
ççg h
;
ççh i
if
éé 
(
éé 
response
éé 
==
éé 
null
éé  $
)
éé$ %
{
èè 
return
êê 

StatusCode
êê %
(
êê% &
$num
êê& )
,
êê) *
$str
êê+ ]
)
êê] ^
;
êê^ _
}
ëë 
return
ìì 
CreatedAtAction
ìì &
(
ìì& '
nameof
îî 
(
îî 
ObtenerInmueble
îî *
)
îî* +
,
îî+ ,
new
ïï 
{
ïï 
id
ïï 
=
ïï 
response
ïï '
.
ïï' (

IdInmueble
ïï( 2
}
ïï3 4
,
ïï4 5
new
ññ 
{
ññ 

idInmueble
ññ $
=
ññ% &
response
ññ' /
.
ññ/ 0

IdInmueble
ññ0 :
,
ññ: ;
mensaje
ññ< C
=
ññD E
$str
ññF f
}
ññg h
)
ññh i
;
ññi j
}
óó 
catch
òò 
(
òò 
ArgumentException
òò $
ex
òò% '
)
òò' (
{
ôô 
return
öö 

StatusCode
öö !
(
öö! "
$num
öö" %
,
öö% &
$"
öö' )
$str
öö) .
{
öö. /
ex
öö/ 1
.
öö1 2
Message
öö2 9
}
öö9 :
"
öö: ;
)
öö; <
;
öö< =
}
õõ 
catch
úú 
(
úú 
	Exception
úú 
ex
úú 
)
úú  
{
ùù 
return
ûû 

StatusCode
ûû !
(
ûû! "
$num
ûû" %
,
ûû% &
$"
ûû' )
$str
ûû) <
{
ûû< =
ex
ûû= ?
.
ûû? @
Message
ûû@ G
}
ûûG H
"
ûûH I
)
ûûI J
;
ûûJ K
}
üü 
}
†† 	
}
°° 
}¢¢ öj
hC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Controllers\InmuebleCaracteristica.cs
	namespace		 	
rever		
 
.		 
Controllers		 
{

 
[ 
Route 

(
 
$str 
) 
] 
[ 
ApiController 
] 
[ 
	Authorize 
( 
Roles 
= 
$str 
) 
] 
public 

class ,
 InmuebleCaracteristicaController 1
:2 3
ControllerBase4 B
{ 
private 
readonly -
!IInmuebleCaracteristicaRepository :-
!_inmueblecaracteristicarepository; \
;\ ]
private 
readonly 
IInmuebleRepository ,
_inmueblerepository- @
;@ A
public ,
 InmuebleCaracteristicaController /
(/ 0-
!IInmuebleCaracteristicaRepository -

repository. 8
,8 9
IInmuebleRepository 
inmuebleRepository  2
)2 3
{ 	-
!_inmueblecaracteristicarepository -
=. /

repository0 :
;: ;
_inmueblerepository 
=  !
inmuebleRepository" 4
;4 5
} 	
[ 	
HttpGet	 
] 
[ 	
AllowAnonymous	 
] 
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *
Status200OK* 5
)5 6
]6 7
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *
Status404NotFound* ;
); <
]< =
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *(
Status500InternalServerError* F
)F G
]G H
public   
async   
Task   
<   
IActionResult   '
>  ' ((
ListarInmuebleCaracteristica  ) E
(  E F
)  F G
{!! 	
try"" 
{## 
var$$ 
response$$ 
=$$ 
await$$ $-
!_inmueblecaracteristicarepository$$% F
.$$F G%
GetInmuebleCaracteristica$$G `
($$` a
)$$a b
;$$b c
if%% 
(%% 
response%% 
==%% 
null%%  $
)%%$ %
{&& 
return'' 

StatusCode'' %
(''% &
$num''& )
,'') *
$str''+ m
)''m n
;''n o
}(( 
return)) 

StatusCode)) !
())! "
$num))" %
,))% &
response))' /
)))/ 0
;))0 1
}** 
catch++ 
(++ 
	Exception++ 
ex++ 
)++  
{,, 
return-- 

StatusCode-- !
(--! "
$num--" %
,--% &
$"--' )
$str--) <
{--< =
ex--= ?
.--? @
Message--@ G
}--G H
"--H I
)--I J
;--J K
}.. 
}// 	
[11 	
HttpGet11	 
(11 
$str11 
)11 
]11  
[22 	
AllowAnonymous22	 
]22 
[33 	 
ProducesResponseType33	 
(33 
StatusCodes33 )
.33) *
Status200OK33* 5
)335 6
]336 7
[44 	 
ProducesResponseType44	 
(44 
StatusCodes44 )
.44) *
Status400BadRequest44* =
)44= >
]44> ?
[55 	 
ProducesResponseType55	 
(55 
StatusCodes55 )
.55) *
Status404NotFound55* ;
)55; <
]55< =
[66 	 
ProducesResponseType66	 
(66 
StatusCodes66 )
.66) *(
Status500InternalServerError66* F
)66F G
]66G H
public77 
async77 
Task77 
<77 
IActionResult77 '
>77' ()
ObtenerInmuebleCaracteristica77) F
(77F G
int77G J
id177K N
,77N O
int77P S
id277T W
)77W X
{88 	
try99 
{:: 
if;; 
(;; 
id1;; 
<=;; 
$num;; 
||;; 
id2;;  #
<=;;$ &
$num;;' (
);;( )
{<< 
return== 

StatusCode== %
(==% &
$num==& )
,==) *
$str==+ X
)==X Y
;==Y Z
}>> 
var@@ 
response@@ 
=@@ 
await@@ $-
!_inmueblecaracteristicarepository@@% F
.@@F G
GetByIds@@G O
(@@O P
id1@@P S
,@@S T
id2@@U X
)@@X Y
;@@Y Z
ifAA 
(AA 
responseAA 
==AA 
nullAA  $
)AA$ %
{BB 
returnCC 

StatusCodeCC %
(CC% &
$numCC& )
,CC) *
$"CC+ -
$strCC- _
{CC_ `
id1CC` c
}CCc d
$strCCd y
{CCy z
id2CCz }
}CC} ~
$strCC~ 
"	CC Ä
)
CCÄ Å
;
CCÅ Ç
}DD 
returnEE 

StatusCodeEE !
(EE! "
$numEE" %
,EE% &
responseEE' /
)EE/ 0
;EE0 1
}FF 
catchGG 
(GG 
	ExceptionGG 
exGG 
)GG  
{HH 
returnII 

StatusCodeII !
(II! "
$numII" %
,II% &
$"II' )
$strII) <
{II< =
exII= ?
.II? @
MessageII@ G
}IIG H
"IIH I
)III J
;IIJ K
}JJ 
}KK 	
[MM 	
HttpPostMM	 
]MM 
[NN 	 
ProducesResponseTypeNN	 
(NN 
StatusCodesNN )
.NN) *
Status200OKNN* 5
)NN5 6
]NN6 7
[OO 	 
ProducesResponseTypeOO	 
(OO 
StatusCodesOO )
.OO) *
Status400BadRequestOO* =
)OO= >
]OO> ?
[PP 	 
ProducesResponseTypePP	 
(PP 
StatusCodesPP )
.PP) *
Status403ForbiddenPP* <
)PP< =
]PP= >
[QQ 	 
ProducesResponseTypeQQ	 
(QQ 
StatusCodesQQ )
.QQ) *
Status404NotFoundQQ* ;
)QQ; <
]QQ< =
[RR 	 
ProducesResponseTypeRR	 
(RR 
StatusCodesRR )
.RR) *(
Status500InternalServerErrorRR* F
)RRF G
]RRG H
publicSS 
asyncSS 
TaskSS 
<SS 
IActionResultSS '
>SS' ('
CrearInmuebleCaracteristicaSS) D
(SSD E
[SSE F
FromBodySSF N
]SSN O"
InmuebleCaracteristicaSSP f"
inmuebleCaracteristicaSSg }
)SS} ~
{TT 	
tryUU 
{VV 
ifWW 
(WW "
inmuebleCaracteristicaWW *
==WW+ -
nullWW. 2
||WW3 5"
inmuebleCaracteristicaWW6 L
.WWL M

IdInmuebleWWM W
<=WWX Z
$numWW[ \
)WW\ ]
{XX 
returnYY 

StatusCodeYY %
(YY% &
$numYY& )
,YY) *
$strYY+ Y
)YYY Z
;YYZ [
}ZZ 
var\\ 
inmueble\\ 
=\\ 
await\\ $
_inmueblerepository\\% 8
.\\8 9
GetInmuebleById\\9 H
(\\H I"
inmuebleCaracteristica\\I _
.\\_ `

IdInmueble\\` j
)\\j k
;\\k l
if]] 
(]] 
inmueble]] 
==]] 
null]]  $
)]]$ %
{^^ 
return__ 

StatusCode__ %
(__% &
$num__& )
,__) *
$"__+ -
$str__- E
{__E F"
inmuebleCaracteristica__F \
.__\ ]

IdInmueble__] g
}__g h
$str__h s
"__s t
)__t u
;__u v
}`` 
ifbb 
(bb 
!bb 
Userbb 
.bb 
EsDue√±oOAdminbb '
(bb' (
inmueblebb( 0
.bb0 1
	IdUsuariobb1 :
)bb: ;
)bb; <
{cc 
returndd 

StatusCodedd %
(dd% &
$numdd& )
,dd) *
$strdd+ q
)ddq r
;ddr s
}ee 
vargg 
responsegg 
=gg 
awaitgg $-
!_inmueblecaracteristicarepositorygg% F
.ggF G&
PostInmuebleCaracteristicaggG a
(gga b"
inmuebleCaracteristicaggb x
)ggx y
;ggy z
ifhh 
(hh 
responsehh 
==hh 
nullhh  $
)hh$ %
{ii 
returnjj 

StatusCodejj %
(jj% &
$numjj& )
,jj) *
$strjj+ ]
)jj] ^
;jj^ _
}kk 
returnll 

StatusCodell !
(ll! "
$numll" %
,ll% &
responsell' /
)ll/ 0
;ll0 1
}mm 
catchnn 
(nn 
	Exceptionnn 
exnn 
)nn  
{oo 
returnpp 

StatusCodepp !
(pp! "
$numpp" %
,pp% &
$"pp' )
$strpp) <
{pp< =
expp= ?
.pp? @
Messagepp@ G
}ppG H
"ppH I
)ppI J
;ppJ K
}qq 
}rr 	
[tt 	

HttpDeletett	 
(tt 
$strtt !
)tt! "
]tt" #
[uu 	 
ProducesResponseTypeuu	 
(uu 
StatusCodesuu )
.uu) *
Status200OKuu* 5
)uu5 6
]uu6 7
[vv 	 
ProducesResponseTypevv	 
(vv 
StatusCodesvv )
.vv) *
Status400BadRequestvv* =
)vv= >
]vv> ?
[ww 	 
ProducesResponseTypeww	 
(ww 
StatusCodesww )
.ww) *
Status403Forbiddenww* <
)ww< =
]ww= >
[xx 	 
ProducesResponseTypexx	 
(xx 
StatusCodesxx )
.xx) *
Status404NotFoundxx* ;
)xx; <
]xx< =
[yy 	 
ProducesResponseTypeyy	 
(yy 
StatusCodesyy )
.yy) *(
Status500InternalServerErroryy* F
)yyF G
]yyG H
publiczz 
asynczz 
Taskzz 
<zz 
IActionResultzz '
>zz' (*
EliminarInmuebleCaracteristicazz) G
(zzG H
intzzH K
id1zzL O
,zzO P
intzzQ T
id2zzU X
)zzX Y
{{{ 	
try|| 
{}} 
if~~ 
(~~ 
id1~~ 
<=~~ 
$num~~ 
||~~ 
id2~~  #
<=~~$ &
$num~~' (
)~~( )
{ 
return
ÄÄ 

StatusCode
ÄÄ %
(
ÄÄ% &
$num
ÄÄ& )
,
ÄÄ) *
$str
ÄÄ+ c
)
ÄÄc d
;
ÄÄd e
}
ÅÅ 
var
ÉÉ 
exist
ÉÉ 
=
ÉÉ 
await
ÉÉ !/
!_inmueblecaracteristicarepository
ÉÉ" C
.
ÉÉC D
GetByIds
ÉÉD L
(
ÉÉL M
id1
ÉÉM P
,
ÉÉP Q
id2
ÉÉR U
)
ÉÉU V
;
ÉÉV W
if
ÑÑ 
(
ÑÑ 
exist
ÑÑ 
==
ÑÑ 
null
ÑÑ !
)
ÑÑ! "
{
ÖÖ 
return
ÜÜ 

StatusCode
ÜÜ %
(
ÜÜ% &
$num
ÜÜ& )
,
ÜÜ) *
$str
ÜÜ+ k
)
ÜÜk l
;
ÜÜl m
}
áá 
var
ââ 
inmueble
ââ 
=
ââ 
await
ââ $!
_inmueblerepository
ââ% 8
.
ââ8 9
GetInmuebleById
ââ9 H
(
ââH I
id1
ââI L
)
ââL M
;
ââM N
if
ää 
(
ää 
inmueble
ää 
==
ää 
null
ää  $
||
ää% '
!
ää( )
User
ää) -
.
ää- .
EsDue√±oOAdmin
ää. ;
(
ää; <
inmueble
ää< D
.
ääD E
	IdUsuario
ääE N
)
ääN O
)
ääO P
{
ãã 
return
åå 

StatusCode
åå %
(
åå% &
$num
åå& )
,
åå) *
$str
åå+ s
)
åås t
;
ååt u
}
çç 
var
èè 
response
èè 
=
èè 
await
èè $/
!_inmueblecaracteristicarepository
èè% F
.
èèF G*
DeleteInmuebleCaracteristica
èèG c
(
èèc d
exist
èèd i
)
èèi j
;
èèj k
return
êê 

StatusCode
êê !
(
êê! "
$num
êê" %
,
êê% &
response
êê' /
)
êê/ 0
;
êê0 1
}
ëë 
catch
íí 
(
íí 
	Exception
íí 
ex
íí 
)
íí  
{
ìì 
return
îî 

StatusCode
îî !
(
îî! "
$num
îî" %
,
îî% &
$"
îî' )
$str
îî) <
{
îî< =
ex
îî= ?
.
îî? @
Message
îî@ G
}
îîG H
"
îîH I
)
îîI J
;
îîJ K
}
ïï 
}
ññ 	
}
óó 
}òò ∆â
bC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Controllers\ImagenController.cs
	namespace		 	
rever		
 
.		 
Controllers		 
{

 
[ 
Route 

(
 
$str 
) 
] 
[ 
ApiController 
] 
[ 
	Authorize 
( 
Roles 
= 
$str 
) 
] 
public 

class 
ImagenController !
:" #
ControllerBase$ 2
{ 
private 
readonly 
IImagenRepository *
_imagenrepository+ <
;< =
private 
readonly 
IInmuebleRepository ,
_inmueblerepository- @
;@ A
public 
ImagenController 
(  
IImagenRepository  1

repository2 <
,< =
IInmuebleRepository> Q
inmuebleRepositoryR d
)d e
{ 	
_imagenrepository 
= 

repository  *
;* +
_inmueblerepository 
=  !
inmuebleRepository" 4
;4 5
} 	
[ 	
HttpGet	 
] 
[ 	
AllowAnonymous	 
] 
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *
Status200OK* 5
)5 6
]6 7
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *
Status404NotFound* ;
); <
]< =
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *(
Status500InternalServerError* F
)F G
]G H
public 
async 
Task 
< 
IActionResult '
>' (
ListarImagen) 5
(5 6
)6 7
{ 	
try   
{!! 
var"" 
response"" 
="" 
await"" $
_imagenrepository""% 6
.""6 7
	GetImagen""7 @
(""@ A
)""A B
;""B C
if## 
(## 
response## 
==## 
null##  $
)##$ %
{$$ 
return%% 

StatusCode%% %
(%%% &
$num%%& )
,%%) *
$str%%+ Y
)%%Y Z
;%%Z [
}&& 
return'' 

StatusCode'' !
(''! "
$num''" %
,''% &
response''' /
)''/ 0
;''0 1
}(( 
catch)) 
()) 
	Exception)) 
ex)) 
)))  
{** 
return++ 

StatusCode++ !
(++! "
$num++" %
,++% &
$"++' )
$str++) <
{++< =
ex++= ?
.++? @
Message++@ G
}++G H
"++H I
)++I J
;++J K
},, 
}-- 	
[// 	
HttpGet//	 
(// 
$str// 
)// 
]// 
[00 	
AllowAnonymous00	 
]00 
[11 	 
ProducesResponseType11	 
(11 
StatusCodes11 )
.11) *
Status200OK11* 5
)115 6
]116 7
[22 	 
ProducesResponseType22	 
(22 
StatusCodes22 )
.22) *
Status400BadRequest22* =
)22= >
]22> ?
[33 	 
ProducesResponseType33	 
(33 
StatusCodes33 )
.33) *
Status404NotFound33* ;
)33; <
]33< =
[44 	 
ProducesResponseType44	 
(44 
StatusCodes44 )
.44) *(
Status500InternalServerError44* F
)44F G
]44G H
public55 
async55 
Task55 
<55 
IActionResult55 '
>55' (
ObtenerImagen55) 6
(556 7
int557 :
id55; =
)55= >
{66 	
try77 
{88 
if99 
(99 
id99 
<=99 
$num99 
)99 
{:: 
return;; 

StatusCode;; %
(;;% &
$num;;& )
,;;) *
$str;;+ S
);;S T
;;;T U
}<< 
var>> 
response>> 
=>> 
await>> $
_imagenrepository>>% 6
.>>6 7
GetImagenById>>7 D
(>>D E
id>>E G
)>>G H
;>>H I
if?? 
(?? 
response?? 
==?? 
null??  $
)??$ %
{@@ 
returnAA 

StatusCodeAA %
(AA% &
$numAA& )
,AA) *
$"AA+ -
$strAA- R
{AAR S
idAAS U
}AAU V
$strAAV W
"AAW X
)AAX Y
;AAY Z
}BB 
returnCC 

StatusCodeCC !
(CC! "
$numCC" %
,CC% &
responseCC' /
)CC/ 0
;CC0 1
}DD 
catchEE 
(EE 
	ExceptionEE 
exEE 
)EE  
{FF 
returnGG 

StatusCodeGG !
(GG! "
$numGG" %
,GG% &
$"GG' )
$strGG) <
{GG< =
exGG= ?
.GG? @
MessageGG@ G
}GGG H
"GGH I
)GGI J
;GGJ K
}HH 
}II 	
[KK 	
HttpPostKK	 
]KK 
[LL 	 
ProducesResponseTypeLL	 
(LL 
StatusCodesLL )
.LL) *
Status200OKLL* 5
)LL5 6
]LL6 7
[MM 	 
ProducesResponseTypeMM	 
(MM 
StatusCodesMM )
.MM) *
Status400BadRequestMM* =
)MM= >
]MM> ?
[NN 	 
ProducesResponseTypeNN	 
(NN 
StatusCodesNN )
.NN) *
Status403ForbiddenNN* <
)NN< =
]NN= >
[OO 	 
ProducesResponseTypeOO	 
(OO 
StatusCodesOO )
.OO) *
Status404NotFoundOO* ;
)OO; <
]OO< =
[PP 	 
ProducesResponseTypePP	 
(PP 
StatusCodesPP )
.PP) *(
Status500InternalServerErrorPP* F
)PPF G
]PPG H
publicQQ 
asyncQQ 
TaskQQ 
<QQ 
IActionResultQQ '
>QQ' (
CrearImagenQQ) 4
(QQ4 5
[QQ5 6
FromBodyQQ6 >
]QQ> ?
ImagenQQ@ F
imagenQQG M
)QQM N
{RR 	
trySS 
{TT 
ifUU 
(UU 
imagenUU 
==UU 
nullUU "
||UU# %
imagenUU& ,
.UU, -

IdInmuebleUU- 7
<=UU8 :
$numUU; <
)UU< =
{VV 
returnWW 

StatusCodeWW %
(WW% &
$numWW& )
,WW) *
$strWW+ ]
)WW] ^
;WW^ _
}XX 
varZZ 
inmuebleZZ 
=ZZ 
awaitZZ $/
#_inmuebleRepository_ObtenerInmuebleZZ% H
(ZZH I
imagenZZI O
.ZZO P

IdInmuebleZZP Z
)ZZZ [
;ZZ[ \
if[[ 
([[ 
inmueble[[ 
==[[ 
null[[  $
)[[$ %
{\\ 
return]] 

StatusCode]] %
(]]% &
$num]]& )
,]]) *
$"]]+ -
$str]]- E
{]]E F
imagen]]F L
.]]L M

IdInmueble]]M W
}]]W X
$str]]X c
"]]c d
)]]d e
;]]e f
}^^ 
if`` 
(`` 
!`` 
User`` 
.`` 
EsDue√±oOAdmin`` '
(``' (
inmueble``( 0
.``0 1
	IdUsuario``1 :
)``: ;
)``; <
{aa 
returnbb 

StatusCodebb %
(bb% &
$numbb& )
,bb) *
$strbb+ j
)bbj k
;bbk l
}cc 
varee 
responseee 
=ee 
awaitee $
_imagenrepositoryee% 6
.ee6 7

PostImagenee7 A
(eeA B
imageneeB H
)eeH I
;eeI J
ifff 
(ff 
responseff 
==ff 
nullff  $
)ff$ %
{gg 
returnhh 

StatusCodehh %
(hh% &
$numhh& )
,hh) *
$strhh+ ]
)hh] ^
;hh^ _
}ii 
returnjj 

StatusCodejj !
(jj! "
$numjj" %
,jj% &
responsejj' /
)jj/ 0
;jj0 1
}kk 
catchll 
(ll 
	Exceptionll 
exll 
)ll  
{mm 
returnnn 

StatusCodenn !
(nn! "
$numnn" %
,nn% &
$"nn' )
$strnn) <
{nn< =
exnn= ?
.nn? @
Messagenn@ G
}nnG H
"nnH I
)nnI J
;nnJ K
}oo 
}pp 	
[rr 	
HttpPutrr	 
]rr 
[ss 	 
ProducesResponseTypess	 
(ss 
StatusCodesss )
.ss) *
Status200OKss* 5
)ss5 6
]ss6 7
[tt 	 
ProducesResponseTypett	 
(tt 
StatusCodestt )
.tt) *
Status400BadRequesttt* =
)tt= >
]tt> ?
[uu 	 
ProducesResponseTypeuu	 
(uu 
StatusCodesuu )
.uu) *
Status403Forbiddenuu* <
)uu< =
]uu= >
[vv 	 
ProducesResponseTypevv	 
(vv 
StatusCodesvv )
.vv) *
Status404NotFoundvv* ;
)vv; <
]vv< =
[ww 	 
ProducesResponseTypeww	 
(ww 
StatusCodesww )
.ww) *(
Status500InternalServerErrorww* F
)wwF G
]wwG H
publicxx 
asyncxx 
Taskxx 
<xx 
IActionResultxx '
>xx' (
ActualizarImagenxx) 9
(xx9 :
[xx: ;
FromBodyxx; C
]xxC D
ImagenxxE K
imagenxxL R
)xxR S
{yy 	
tryzz 
{{{ 
if|| 
(|| 
imagen|| 
==|| 
null|| "
||||# %
imagen||& ,
.||, -
IdImagen||- 5
<=||6 8
$num||9 :
)||: ;
{}} 
return~~ 

StatusCode~~ %
(~~% &
$num~~& )
,~~) *
$str~~+ c
)~~c d
;~~d e
} 
var
ÅÅ 
exist
ÅÅ 
=
ÅÅ 
await
ÅÅ !
_imagenrepository
ÅÅ" 3
.
ÅÅ3 4
GetImagenById
ÅÅ4 A
(
ÅÅA B
imagen
ÅÅB H
.
ÅÅH I
IdImagen
ÅÅI Q
)
ÅÅQ R
;
ÅÅR S
if
ÇÇ 
(
ÇÇ 
exist
ÇÇ 
==
ÇÇ 
null
ÇÇ !
)
ÇÇ! "
{
ÉÉ 
return
ÑÑ 

StatusCode
ÑÑ %
(
ÑÑ% &
$num
ÑÑ& )
,
ÑÑ) *
$"
ÑÑ+ -
$str
ÑÑ- [
{
ÑÑ[ \
imagen
ÑÑ\ b
.
ÑÑb c
IdImagen
ÑÑc k
}
ÑÑk l
$str
ÑÑl w
"
ÑÑw x
)
ÑÑx y
;
ÑÑy z
}
ÖÖ 
var
áá 
inmueble
áá 
=
áá 
await
áá $1
#_inmuebleRepository_ObtenerInmueble
áá% H
(
ááH I
exist
ááI N
.
ááN O

IdInmueble
ááO Y
)
ááY Z
;
ááZ [
if
àà 
(
àà 
inmueble
àà 
==
àà 
null
àà  $
||
àà% '
!
àà( )
User
àà) -
.
àà- .
EsDue√±oOAdmin
àà. ;
(
àà; <
inmueble
àà< D
.
ààD E
	IdUsuario
ààE N
)
ààN O
)
ààO P
{
ââ 
return
ää 

StatusCode
ää %
(
ää% &
$num
ää& )
,
ää) *
$str
ää+ m
)
ääm n
;
ään o
}
ãã 
exist
çç 
.
çç 
Url
çç 
=
çç 
imagen
çç "
.
çç" #
Url
çç# &
;
çç& '
exist
éé 
.
éé 

IdInmueble
éé  
=
éé! "
imagen
éé# )
.
éé) *

IdInmueble
éé* 4
;
éé4 5
var
èè 
response
èè 
=
èè 
await
èè $
_imagenrepository
èè% 6
.
èè6 7
	PutImagen
èè7 @
(
èè@ A
exist
èèA F
)
èèF G
;
èèG H
return
êê 

StatusCode
êê !
(
êê! "
$num
êê" %
,
êê% &
response
êê' /
)
êê/ 0
;
êê0 1
}
ëë 
catch
íí 
(
íí 
	Exception
íí 
ex
íí 
)
íí  
{
ìì 
return
îî 

StatusCode
îî !
(
îî! "
$num
îî" %
,
îî% &
$"
îî' )
$str
îî) <
{
îî< =
ex
îî= ?
.
îî? @
Message
îî@ G
}
îîG H
"
îîH I
)
îîI J
;
îîJ K
}
ïï 
}
ññ 	
[
òò 	

HttpDelete
òò	 
(
òò 
$str
òò 
)
òò 
]
òò 
[
ôô 	"
ProducesResponseType
ôô	 
(
ôô 
StatusCodes
ôô )
.
ôô) *
Status200OK
ôô* 5
)
ôô5 6
]
ôô6 7
[
öö 	"
ProducesResponseType
öö	 
(
öö 
StatusCodes
öö )
.
öö) *!
Status400BadRequest
öö* =
)
öö= >
]
öö> ?
[
õõ 	"
ProducesResponseType
õõ	 
(
õõ 
StatusCodes
õõ )
.
õõ) * 
Status403Forbidden
õõ* <
)
õõ< =
]
õõ= >
[
úú 	"
ProducesResponseType
úú	 
(
úú 
StatusCodes
úú )
.
úú) *
Status404NotFound
úú* ;
)
úú; <
]
úú< =
[
ùù 	"
ProducesResponseType
ùù	 
(
ùù 
StatusCodes
ùù )
.
ùù) **
Status500InternalServerError
ùù* F
)
ùùF G
]
ùùG H
public
ûû 
async
ûû 
Task
ûû 
<
ûû 
IActionResult
ûû '
>
ûû' (
DeleteImagen
ûû) 5
(
ûû5 6
int
ûû6 9
id
ûû: <
)
ûû< =
{
üü 	
try
†† 
{
°° 
if
¢¢ 
(
¢¢ 
id
¢¢ 
<=
¢¢ 
$num
¢¢ 
)
¢¢ 
{
££ 
return
§§ 

StatusCode
§§ %
(
§§% &
$num
§§& )
,
§§) *
$str
§§+ R
)
§§R S
;
§§S T
}
•• 
var
ßß 
exist
ßß 
=
ßß 
await
ßß !
_imagenrepository
ßß" 3
.
ßß3 4
GetImagenById
ßß4 A
(
ßßA B
id
ßßB D
)
ßßD E
;
ßßE F
if
®® 
(
®® 
exist
®® 
==
®® 
null
®® !
)
®®! "
{
©© 
return
™™ 

StatusCode
™™ %
(
™™% &
$num
™™& )
,
™™) *
$"
™™+ -
$str
™™- Y
{
™™Y Z
id
™™Z \
}
™™\ ]
$str
™™] h
"
™™h i
)
™™i j
;
™™j k
}
´´ 
var
≠≠ 
inmueble
≠≠ 
=
≠≠ 
await
≠≠ $1
#_inmuebleRepository_ObtenerInmueble
≠≠% H
(
≠≠H I
exist
≠≠I N
.
≠≠N O

IdInmueble
≠≠O Y
)
≠≠Y Z
;
≠≠Z [
if
ÆÆ 
(
ÆÆ 
inmueble
ÆÆ 
==
ÆÆ 
null
ÆÆ  $
||
ÆÆ% '
!
ÆÆ( )
User
ÆÆ) -
.
ÆÆ- .
EsDue√±oOAdmin
ÆÆ. ;
(
ÆÆ; <
inmueble
ÆÆ< D
.
ÆÆD E
	IdUsuario
ÆÆE N
)
ÆÆN O
)
ÆÆO P
{
ØØ 
return
∞∞ 

StatusCode
∞∞ %
(
∞∞% &
$num
∞∞& )
,
∞∞) *
$str
∞∞+ l
)
∞∞l m
;
∞∞m n
}
±± 
var
≥≥ 
response
≥≥ 
=
≥≥ 
await
≥≥ $
_imagenrepository
≥≥% 6
.
≥≥6 7
DeleteImagen
≥≥7 C
(
≥≥C D
exist
≥≥D I
)
≥≥I J
;
≥≥J K
return
¥¥ 

StatusCode
¥¥ !
(
¥¥! "
$num
¥¥" %
,
¥¥% &
response
¥¥' /
)
¥¥/ 0
;
¥¥0 1
}
µµ 
catch
∂∂ 
(
∂∂ 
	Exception
∂∂ 
)
∂∂ 
{
∑∑ 
return
∏∏ 

StatusCode
∏∏ !
(
∏∏! "
$num
∏∏" %
,
∏∏% &
$str
∏∏' I
)
∏∏I J
;
∏∏J K
}
ππ 
}
∫∫ 	
private
ºº 
async
ºº 
Task
ºº 
<
ºº 
Inmueble
ºº #
?
ºº# $
>
ºº$ %1
#_inmuebleRepository_ObtenerInmueble
ºº& I
(
ººI J
int
ººJ M

idInmueble
ººN X
)
ººX Y
{
ΩΩ 	
return
ææ 
await
ææ !
_inmueblerepository
ææ ,
.
ææ, -
GetInmuebleById
ææ- <
(
ææ< =

idInmueble
ææ= G
)
ææG H
;
ææH I
}
øø 	
}
¡¡ 
}¬¬ ƒ|
mC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Controllers\EstadoPublicacionController.cs
	namespace		 	
rever		
 
.		 
Controllers		 
{

 
[ 
Route 

(
 
$str 
) 
] 
[ 
ApiController 
] 
[ 
	Authorize 
( 
Roles 
= 
$str 
) 
] 
public 

class '
EstadoPublicacionController ,
:- .
ControllerBase/ =
{ 
private 
readonly (
IEstadoPublicacionRepository 5(
_estadopublicacionrepository6 R
;R S
public '
EstadoPublicacionController *
(* +(
IEstadoPublicacionRepository+ G

repositoryH R
)R S
{ 	(
_estadopublicacionrepository (
=) *

repository+ 5
;5 6
} 	
[ 	
HttpGet	 
] 
[ 	
AllowAnonymous	 
] 
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *
Status200OK* 5
)5 6
]6 7
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *!
Status401Unauthorized* ?
)? @
]@ A
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *
Status404NotFound* ;
); <
]< =
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *(
Status500InternalServerError* F
)F G
]G H
public 
async 
Task 
< 
IActionResult '
>' (#
ListarEstadoPublicacion) @
(@ A
)A B
{ 	
try 
{ 
var   
response   
=   
await   $(
_estadopublicacionrepository  % A
.  A B 
GetEstadoPublicacion  B V
(  V W
)  W X
;  X Y
if!! 
(!! 
response!! 
==!! 
null!!  $
)!!$ %
{"" 
return## 

StatusCode## %
(##% &
$num##& )
,##) *
$str##+ g
)##g h
;##h i
}$$ 
return%% 

StatusCode%% !
(%%! "
$num%%" %
,%%% &
response%%' /
)%%/ 0
;%%0 1
}&& 
catch'' 
('' 
	Exception'' 
ex'' 
)''  
{(( 
return)) 

StatusCode)) !
())! "
$num))" %
,))% &
$"))' )
$str))) <
{))< =
ex))= ?
.))? @
Message))@ G
}))G H
"))H I
)))I J
;))J K
}** 
}++ 	
[-- 	
HttpGet--	 
(-- 
$str-- 
)-- 
]-- 
[.. 	
AllowAnonymous..	 
].. 
[// 	 
ProducesResponseType//	 
(// 
StatusCodes// )
.//) *
Status200OK//* 5
)//5 6
]//6 7
[00 	 
ProducesResponseType00	 
(00 
StatusCodes00 )
.00) *
Status400BadRequest00* =
)00= >
]00> ?
[11 	 
ProducesResponseType11	 
(11 
StatusCodes11 )
.11) *!
Status401Unauthorized11* ?
)11? @
]11@ A
[22 	 
ProducesResponseType22	 
(22 
StatusCodes22 )
.22) *
Status404NotFound22* ;
)22; <
]22< =
[33 	 
ProducesResponseType33	 
(33 
StatusCodes33 )
.33) *(
Status500InternalServerError33* F
)33F G
]33G H
public44 
async44 
Task44 
<44 
IActionResult44 '
>44' ($
ObtenerEstadoPublicacion44) A
(44A B
int44B E
id44F H
)44H I
{55 	
try66 
{77 
if88 
(88 
id88 
<=88 
$num88 
)88 
{99 
return:: 

StatusCode:: %
(::% &
$num::& )
,::) *
$str::+ S
)::S T
;::T U
};; 
var== 
response== 
=== 
await== $(
_estadopublicacionrepository==% A
.==A B$
GetEstadoPublicacionById==B Z
(==Z [
id==[ ]
)==] ^
;==^ _
if>> 
(>> 
response>> 
==>> 
null>>  $
)>>$ %
{?? 
return@@ 

StatusCode@@ %
(@@% &
$num@@& )
,@@) *
$"@@+ -
$str@@- a
{@@a b
id@@b d
}@@d e
$str@@e f
"@@f g
)@@g h
;@@h i
}AA 
returnBB 

StatusCodeBB !
(BB! "
$numBB" %
,BB% &
responseBB' /
)BB/ 0
;BB0 1
}CC 
catchDD 
(DD 
	ExceptionDD 
exDD 
)DD  
{EE 
returnFF 

StatusCodeFF !
(FF! "
$numFF" %
,FF% &
$"FF' )
$strFF) <
{FF< =
exFF= ?
.FF? @
MessageFF@ G
}FFG H
"FFH I
)FFI J
;FFJ K
}GG 
}II 	
[KK 	
HttpPostKK	 
]KK 
[LL 	 
ProducesResponseTypeLL	 
(LL 
StatusCodesLL )
.LL) *
Status200OKLL* 5
)LL5 6
]LL6 7
[MM 	 
ProducesResponseTypeMM	 
(MM 
StatusCodesMM )
.MM) *
Status400BadRequestMM* =
)MM= >
]MM> ?
[NN 	 
ProducesResponseTypeNN	 
(NN 
StatusCodesNN )
.NN) *!
Status401UnauthorizedNN* ?
)NN? @
]NN@ A
[OO 	 
ProducesResponseTypeOO	 
(OO 
StatusCodesOO )
.OO) *(
Status500InternalServerErrorOO* F
)OOF G
]OOG H
publicPP 
asyncPP 
TaskPP 
<PP 
IActionResultPP '
>PP' ("
CrearEstadoPublicacionPP) ?
(PP? @
[PP@ A
FromBodyPPA I
]PPI J
EstadoPublicacionPPK \
estadopublicacionPP] n
)PPn o
{QQ 	
tryRR 
{SS 
ifTT 
(TT 
UserTT 
.TT 
IdentityTT !
==TT" $
nullTT% )
||TT* ,
!TT- .
UserTT. 2
.TT2 3
IdentityTT3 ;
.TT; <
IsAuthenticatedTT< K
)TTK L
{UU 
returnVV 

StatusCodeVV %
(VV% &
$numVV& )
,VV) *
$strVV+ I
)VVI J
;VVJ K
}WW 
ifYY 
(YY 
estadopublicacionYY %
==YY& (
nullYY) -
)YY- .
{ZZ 
return[[ 

StatusCode[[ %
([[% &
$num[[& )
,[[) *
$str[[+ j
)[[j k
;[[k l
}\\ 
var^^ 
response^^ 
=^^ 
await^^ $(
_estadopublicacionrepository^^% A
.^^A B!
PostEstadoPublicacion^^B W
(^^W X
estadopublicacion^^X i
)^^i j
;^^j k
if__ 
(__ 
response__ 
==__ 
null__  $
)__$ %
{`` 
returnaa 

StatusCodeaa %
(aa% &
$numaa& )
,aa) *
$straa+ ]
)aa] ^
;aa^ _
}bb 
returncc 

StatusCodecc !
(cc! "
$numcc" %
,cc% &
responsecc' /
)cc/ 0
;cc0 1
}dd 
catchee 
(ee 
	Exceptionee 
exee 
)ee  
{ff 
returngg 

StatusCodegg !
(gg! "
$numgg" %
,gg% &
$"gg' )
$strgg) <
{gg< =
exgg= ?
.gg? @
Messagegg@ G
}ggG H
"ggH I
)ggI J
;ggJ K
}hh 
}ii 	
[kk 	
HttpPutkk	 
]kk 
[ll 	 
ProducesResponseTypell	 
(ll 
StatusCodesll )
.ll) *
Status200OKll* 5
)ll5 6
]ll6 7
[mm 	 
ProducesResponseTypemm	 
(mm 
StatusCodesmm )
.mm) *
Status400BadRequestmm* =
)mm= >
]mm> ?
[nn 	 
ProducesResponseTypenn	 
(nn 
StatusCodesnn )
.nn) *!
Status401Unauthorizednn* ?
)nn? @
]nn@ A
[oo 	 
ProducesResponseTypeoo	 
(oo 
StatusCodesoo )
.oo) *
Status404NotFoundoo* ;
)oo; <
]oo< =
[pp 	 
ProducesResponseTypepp	 
(pp 
StatusCodespp )
.pp) *(
Status500InternalServerErrorpp* F
)ppF G
]ppG H
publicqq 
asyncqq 
Taskqq 
<qq 
IActionResultqq '
>qq' ('
ActualizarEstadoPublicacionqq) D
(qqD E
[qqE F
FromBodyqqF N
]qqN O
EstadoPublicacionqqP a
estadopublicacionqqb s
)qqs t
{rr 	
tryss 
{tt 
ifuu 
(uu 
Useruu 
.uu 
Identityuu !
==uu" $
nulluu% )
||uu* ,
!uu- .
Useruu. 2
.uu2 3
Identityuu3 ;
.uu; <
IsAuthenticateduu< K
)uuK L
{vv 
returnww 

StatusCodeww %
(ww% &
$numww& )
,ww) *
$strww+ I
)wwI J
;wwJ K
}xx 
ifzz 
(zz 
estadopublicacionzz %
==zz& (
nullzz) -
||zz. 0
estadopublicacionzz1 B
.zzB C
IdEstadozzC K
<=zzL N
$numzzO P
)zzP Q
{{{ 
return|| 

StatusCode|| %
(||% &
$num||& )
,||) *
$str||+ c
)||c d
;||d e
}}} 
var 
exist 
= 
await !(
_estadopublicacionrepository" >
.> ?$
GetEstadoPublicacionById? W
(W X
estadopublicacionX i
.i j
IdEstadoj r
)r s
;s t
if
ÄÄ 
(
ÄÄ 
exist
ÄÄ 
==
ÄÄ 
null
ÄÄ !
)
ÄÄ! "
{
ÅÅ 
return
ÇÇ 

StatusCode
ÇÇ %
(
ÇÇ% &
$num
ÇÇ& )
,
ÇÇ) *
$"
ÇÇ+ -
$str
ÇÇ- j
{
ÇÇj k
estadopublicacion
ÇÇk |
.
ÇÇ| }
IdEstadoÇÇ} Ö
}ÇÇÖ Ü
$strÇÇÜ ë
"ÇÇë í
)ÇÇí ì
;ÇÇì î
}
ÉÉ 
exist
ÖÖ 
.
ÖÖ 
Nombre
ÖÖ 
=
ÖÖ 
estadopublicacion
ÖÖ 0
.
ÖÖ0 1
Nombre
ÖÖ1 7
;
ÖÖ7 8
var
ÜÜ 
response
ÜÜ 
=
ÜÜ 
await
ÜÜ $*
_estadopublicacionrepository
ÜÜ% A
.
ÜÜA B"
PutEstadoPublicacion
ÜÜB V
(
ÜÜV W
exist
ÜÜW \
)
ÜÜ\ ]
;
ÜÜ] ^
return
áá 

StatusCode
áá !
(
áá! "
$num
áá" %
,
áá% &
response
áá' /
)
áá/ 0
;
áá0 1
}
àà 
catch
ââ 
(
ââ 
	Exception
ââ 
ex
ââ 
)
ââ  
{
ää 
return
ãã 

StatusCode
ãã !
(
ãã! "
$num
ãã" %
,
ãã% &
$"
ãã' )
$str
ãã) <
{
ãã< =
ex
ãã= ?
.
ãã? @
Message
ãã@ G
}
ããG H
"
ããH I
)
ããI J
;
ããJ K
}
åå 
}
çç 	
[
èè 	

HttpDelete
èè	 
(
èè 
$str
èè 
)
èè 
]
èè 
[
êê 	"
ProducesResponseType
êê	 
(
êê 
StatusCodes
êê )
.
êê) *
Status200OK
êê* 5
)
êê5 6
]
êê6 7
[
ëë 	"
ProducesResponseType
ëë	 
(
ëë 
StatusCodes
ëë )
.
ëë) *!
Status400BadRequest
ëë* =
)
ëë= >
]
ëë> ?
[
íí 	"
ProducesResponseType
íí	 
(
íí 
StatusCodes
íí )
.
íí) *#
Status401Unauthorized
íí* ?
)
íí? @
]
íí@ A
[
ìì 	"
ProducesResponseType
ìì	 
(
ìì 
StatusCodes
ìì )
.
ìì) *
Status404NotFound
ìì* ;
)
ìì; <
]
ìì< =
[
îî 	"
ProducesResponseType
îî	 
(
îî 
StatusCodes
îî )
.
îî) **
Status500InternalServerError
îî* F
)
îîF G
]
îîG H
public
ïï 
async
ïï 
Task
ïï 
<
ïï 
IActionResult
ïï '
>
ïï' (%
DeleteEstadoPublicacion
ïï) @
(
ïï@ A
int
ïïA D
id
ïïE G
)
ïïG H
{
ññ 	
try
óó 
{
òò 
if
ôô 
(
ôô 
User
ôô 
.
ôô 
Identity
ôô !
==
ôô" $
null
ôô% )
||
ôô* ,
!
ôô- .
User
ôô. 2
.
ôô2 3
Identity
ôô3 ;
.
ôô; <
IsAuthenticated
ôô< K
)
ôôK L
{
öö 
return
õõ 

StatusCode
õõ %
(
õõ% &
$num
õõ& )
,
õõ) *
$str
õõ+ W
)
õõW X
;
õõX Y
}
úú 
if
ûû 
(
ûû 
id
ûû 
<=
ûû 
$num
ûû 
)
ûû 
{
üü 
return
†† 

StatusCode
†† %
(
††% &
$num
††& )
,
††) *
$str
††+ _
)
††_ `
;
††` a
}
°° 
var
££ 
exist
££ 
=
££ 
await
££ !*
_estadopublicacionrepository
££" >
.
££> ?&
GetEstadoPublicacionById
££? W
(
££W X
id
££X Z
)
££Z [
;
££[ \
if
•• 
(
•• 
exist
•• 
==
•• 
null
•• !
)
••! "
{
¶¶ 
return
ßß 

StatusCode
ßß %
(
ßß% &
$num
®® 
,
®® 
$"
©© 
$str
©© U
{
©©U V
id
©©V X
}
©©X Y
$str
©©Y d
"
©©d e
)
™™ 
;
™™ 
}
´´ 
var
≠≠ 
response
≠≠ 
=
≠≠ 
await
≠≠ $*
_estadopublicacionrepository
≠≠% A
.
≠≠A B%
DeleteEstadoPublicacion
≠≠B Y
(
≠≠Y Z
exist
≠≠Z _
)
≠≠_ `
;
≠≠` a
return
ØØ 

StatusCode
ØØ !
(
ØØ! "
$num
ØØ" %
,
ØØ% &
response
ØØ' /
)
ØØ/ 0
;
ØØ0 1
}
∞∞ 
catch
±± 
(
±± 
	Exception
±± 
)
±± 
{
≤≤ 
return
≥≥ 

StatusCode
≥≥ !
(
≥≥! "
$num
≥≥" %
,
≥≥% &
$str
≥≥' I
)
≥≥I J
;
≥≥J K
}
¥¥ 
}
µµ 	
}
∂∂ 
}∑∑ ∞å
dC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Controllers\ContactoController.cs
	namespace 	
rever
 
. 
Controllers 
{ 
[ 
Route 

(
 
$str 
) 
] 
[ 
ApiController 
] 
[ 
	Authorize 
( 
Roles 
= 
$str 
) 
]  
public 

class 
ContactoController #
:$ %
ControllerBase& 4
{ 
private 
readonly 
IContactoRepository ,
_contactorepository- @
;@ A
public 
ContactoController !
(! "
IContactoRepository" 5

repository6 @
)@ A
{ 	
_contactorepository 
=  !

repository" ,
;, -
} 	
[ 	
HttpGet	 
] 
[ 	
	Authorize	 
( 
Roles 
= 
$str 
) 
]  
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *
Status200OK* 5
)5 6
]6 7
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *!
Status401Unauthorized* ?
)? @
]@ A
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *
Status404NotFound* ;
); <
]< =
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *(
Status500InternalServerError* F
)F G
]G H
public 
async 
Task 
< 
IActionResult '
>' (
ListarContacto) 7
(7 8
)8 9
{   	
try!! 
{"" 
if## 
(## 
User## 
.## 
Identity## !
==##" $
null##% )
||##* ,
!##- .
User##. 2
.##2 3
Identity##3 ;
.##; <
IsAuthenticated##< K
)##K L
{$$ 
return%% 

StatusCode%% %
(%%% &
$num%%& )
,%%) *
$str%%+ I
)%%I J
;%%J K
}&& 
var(( 
response(( 
=(( 
await(( $
_contactorepository((% 8
.((8 9
GetContacto((9 D
(((D E
)((E F
;((F G
if)) 
()) 
response)) 
==)) 
null))  $
)))$ %
{** 
return++ 

StatusCode++ %
(++% &
$num++& )
,++) *
$str+++ Z
)++Z [
;++[ \
},, 
return-- 

StatusCode-- !
(--! "
$num--" %
,--% &
response--' /
)--/ 0
;--0 1
}.. 
catch// 
(// 
	Exception// 
ex// 
)//  
{00 
return11 

StatusCode11 !
(11! "
$num11" %
,11% &
$"11' )
$str11) <
{11< =
ex11= ?
.11? @
Message11@ G
}11G H
"11H I
)11I J
;11J K
}22 
}33 	
[55 	
HttpGet55	 
(55 
$str55 
)55 
]55 
[66 	 
ProducesResponseType66	 
(66 
StatusCodes66 )
.66) *
Status200OK66* 5
)665 6
]666 7
[77 	 
ProducesResponseType77	 
(77 
StatusCodes77 )
.77) *
Status400BadRequest77* =
)77= >
]77> ?
[88 	 
ProducesResponseType88	 
(88 
StatusCodes88 )
.88) *!
Status401Unauthorized88* ?
)88? @
]88@ A
[99 	 
ProducesResponseType99	 
(99 
StatusCodes99 )
.99) *
Status404NotFound99* ;
)99; <
]99< =
[:: 	 
ProducesResponseType::	 
(:: 
StatusCodes:: )
.::) *(
Status500InternalServerError::* F
)::F G
]::G H
public;; 
async;; 
Task;; 
<;; 
IActionResult;; '
>;;' (
ObtenerContacto;;) 8
(;;8 9
int;;9 <
id;;= ?
);;? @
{<< 	
try== 
{>> 
if?? 
(?? 
id?? 
<=?? 
$num?? 
)?? 
{@@ 
returnAA 

StatusCodeAA %
(AA% &
$numAA& )
,AA) *
$strAA+ S
)AAS T
;AAT U
}BB 
varDD 
responseDD 
=DD 
awaitDD $
_contactorepositoryDD% 8
.DD8 9
GetContactoByIdDD9 H
(DDH I
idDDI K
)DDK L
;DDL M
ifEE 
(EE 
responseEE 
==EE 
nullEE  $
)EE$ %
{FF 
returnGG 

StatusCodeGG %
(GG% &
$numGG& )
,GG) *
$"GG+ -
$strGG- T
{GGT U
idGGU W
}GGW X
$strGGX Y
"GGY Z
)GGZ [
;GG[ \
}HH 
varKK 
idUsuarioTokenKK "
=KK# $
UserKK% )
.KK) *
ObtenerIdUsuarioKK* :
(KK: ;
)KK; <
;KK< =
ifMM 
(MM 
!MM 
idUsuarioTokenMM #
.MM# $
HasValueMM$ ,
)MM, -
{NN 
returnOO 

StatusCodeOO %
(OO% &
$numOO& )
,OO) *
$strOO+ c
)OOc d
;OOd e
}PP 
varSS 
esAdminSS 
=SS 
UserSS "
.SS" #
EsAdministradorSS# 2
(SS2 3
)SS3 4
;SS4 5
varTT 
esParteDelMensajeTT %
=TT& '
responseTT( 0
.TT0 1
IdCompradorTT1 <
==TT= ?
idUsuarioTokenTT@ N
.TTN O
ValueTTO T
||TTU W
responseTTX `
.TT` a

IdVendedorTTa k
==TTl n
idUsuarioTokenTTo }
.TT} ~
Value	TT~ É
;
TTÉ Ñ
ifVV 
(VV 
!VV 
esParteDelMensajeVV &
&&VV' )
!VV* +
esAdminVV+ 2
)VV2 3
{WW 
returnXX 

StatusCodeXX %
(XX% &
$numXX& )
,XX) *
$strXX+ _
)XX_ `
;XX` a
}YY 
return[[ 

StatusCode[[ !
([[! "
$num[[" %
,[[% &
response[[' /
)[[/ 0
;[[0 1
}\\ 
catch]] 
(]] 
	Exception]] 
ex]] 
)]]  
{^^ 
return__ 

StatusCode__ !
(__! "
$num__" %
,__% &
$"__' )
$str__) <
{__< =
ex__= ?
.__? @
Message__@ G
}__G H
"__H I
)__I J
;__J K
}`` 
}aa 	
[cc 	
HttpPostcc	 
]cc 
publicdd 
asyncdd 
Taskdd 
<dd 
IActionResultdd '
>dd' (
CrearContactodd) 6
(dd6 7
[dd7 8
FromBodydd8 @
]dd@ A
ContactoddB J
contactoddK S
)ddS T
{ee 	
tryff 
{gg 
ifhh 
(hh 
contactohh 
==hh 
nullhh  $
)hh$ %
{ii 
returnjj 

StatusCodejj %
(jj% &
$numjj& )
,jj) *
$strjj+ ]
)jj] ^
;jj^ _
}kk 
varnn 
idUsuarioTokennn "
=nn# $
Usernn% )
.nn) *
ObtenerIdUsuarionn* :
(nn: ;
)nn; <
;nn< =
ifpp 
(pp 
!pp 
idUsuarioTokenpp #
.pp# $
HasValuepp$ ,
)pp, -
{qq 
returnrr 

StatusCoderr %
(rr% &
$numrr& )
,rr) *
$strrr+ c
)rrc d
;rrd e
}ss 
contactovv 
.vv 
IdCompradorvv $
=vv% &
idUsuarioTokenvv' 5
.vv5 6
Valuevv6 ;
;vv; <
contactoww 
.ww 
Fechaww 
=ww  
DateTimeww! )
.ww) *
UtcNowww* 0
;ww0 1
varyy 
responseyy 
=yy 
awaityy $
_contactorepositoryyy% 8
.yy8 9
PostContactoyy9 E
(yyE F
contactoyyF N
)yyN O
;yyO P
ifzz 
(zz 
responsezz 
==zz 
nullzz  $
)zz$ %
{{{ 
return|| 

StatusCode|| %
(||% &
$num||& )
,||) *
$str||+ ]
)||] ^
;||^ _
}}} 
return 

StatusCode !
(! "
$num" %
,% &
response' /
)/ 0
;0 1
}
ÄÄ 
catch
ÅÅ 
(
ÅÅ 
	Exception
ÅÅ 
ex
ÅÅ 
)
ÅÅ  
{
ÇÇ 
return
ÉÉ 

StatusCode
ÉÉ !
(
ÉÉ! "
$num
ÉÉ" %
,
ÉÉ% &
$"
ÉÉ' )
$str
ÉÉ) <
{
ÉÉ< =
ex
ÉÉ= ?
.
ÉÉ? @
Message
ÉÉ@ G
}
ÉÉG H
"
ÉÉH I
)
ÉÉI J
;
ÉÉJ K
}
ÑÑ 
}
ÖÖ 	
[
áá 	
HttpPut
áá	 
]
áá 
[
àà 	"
ProducesResponseType
àà	 
(
àà 
StatusCodes
àà )
.
àà) *
Status200OK
àà* 5
)
àà5 6
]
àà6 7
[
ââ 	"
ProducesResponseType
ââ	 
(
ââ 
StatusCodes
ââ )
.
ââ) *!
Status400BadRequest
ââ* =
)
ââ= >
]
ââ> ?
[
ää 	"
ProducesResponseType
ää	 
(
ää 
StatusCodes
ää )
.
ää) *#
Status401Unauthorized
ää* ?
)
ää? @
]
ää@ A
[
ãã 	"
ProducesResponseType
ãã	 
(
ãã 
StatusCodes
ãã )
.
ãã) *
Status404NotFound
ãã* ;
)
ãã; <
]
ãã< =
[
åå 	"
ProducesResponseType
åå	 
(
åå 
StatusCodes
åå )
.
åå) **
Status500InternalServerError
åå* F
)
ååF G
]
ååG H
public
çç 
async
çç 
Task
çç 
<
çç 
IActionResult
çç '
>
çç' ( 
ActualizarContacto
çç) ;
(
çç; <
[
çç< =
FromBody
çç= E
]
ççE F
Contacto
ççG O
contacto
ççP X
)
ççX Y
{
éé 	
try
èè 
{
êê 
if
ëë 
(
ëë 
User
ëë 
.
ëë 
Identity
ëë !
==
ëë" $
null
ëë% )
||
ëë* ,
!
ëë- .
User
ëë. 2
.
ëë2 3
Identity
ëë3 ;
.
ëë; <
IsAuthenticated
ëë< K
)
ëëK L
{
íí 
return
ìì 

StatusCode
ìì %
(
ìì% &
$num
ìì& )
,
ìì) *
$str
ìì+ I
)
ììI J
;
ììJ K
}
îî 
if
ññ 
(
ññ 
contacto
ññ 
==
ññ 
null
ññ  $
||
ññ% '
contacto
ññ( 0
.
ññ0 1

IdContacto
ññ1 ;
<=
ññ< >
$num
ññ? @
)
ññ@ A
{
óó 
return
òò 

StatusCode
òò %
(
òò% &
$num
òò& )
,
òò) *
$str
òò+ c
)
òòc d
;
òòd e
}
ôô 
var
õõ 
exist
õõ 
=
õõ 
await
õõ !!
_contactorepository
õõ" 5
.
õõ5 6
GetContactoById
õõ6 E
(
õõE F
contacto
õõF N
.
õõN O

IdContacto
õõO Y
)
õõY Z
;
õõZ [
if
úú 
(
úú 
exist
úú 
==
úú 
null
úú !
)
úú! "
{
ùù 
return
ûû 

StatusCode
ûû %
(
ûû% &
$num
ûû& )
,
ûû) *
$"
ûû+ -
$str
ûû- ]
{
ûû] ^
contacto
ûû^ f
.
ûûf g

IdContacto
ûûg q
}
ûûq r
$str
ûûr }
"
ûû} ~
)
ûû~ 
;ûû Ä
}
üü 
exist
°° 
.
°° 
IdComprador
°° !
=
°°" #
contacto
°°$ ,
.
°°, -
IdComprador
°°- 8
;
°°8 9
exist
¢¢ 
.
¢¢ 

IdVendedor
¢¢  
=
¢¢! "
contacto
¢¢# +
.
¢¢+ ,

IdVendedor
¢¢, 6
;
¢¢6 7
exist
££ 
.
££ 

IdInmueble
££  
=
££! "
contacto
££# +
.
££+ ,

IdInmueble
££, 6
;
££6 7
exist
§§ 
.
§§ 
Mensaje
§§ 
=
§§ 
contacto
§§  (
.
§§( )
Mensaje
§§) 0
;
§§0 1
exist
•• 
.
•• 
Fecha
•• 
=
•• 
contacto
•• &
.
••& '
Fecha
••' ,
;
••, -
var
¶¶ 
response
¶¶ 
=
¶¶ 
await
¶¶ $!
_contactorepository
¶¶% 8
.
¶¶8 9
PutContacto
¶¶9 D
(
¶¶D E
exist
¶¶E J
)
¶¶J K
;
¶¶K L
return
ßß 

StatusCode
ßß !
(
ßß! "
$num
ßß" %
,
ßß% &
response
ßß' /
)
ßß/ 0
;
ßß0 1
}
®® 
catch
©© 
(
©© 
	Exception
©© 
ex
©© 
)
©©  
{
™™ 
return
´´ 

StatusCode
´´ !
(
´´! "
$num
´´" %
,
´´% &
$"
´´' )
$str
´´) <
{
´´< =
ex
´´= ?
.
´´? @
Message
´´@ G
}
´´G H
"
´´H I
)
´´I J
;
´´J K
}
¨¨ 
}
≠≠ 	
[
ØØ 	

HttpDelete
ØØ	 
(
ØØ 
$str
ØØ 
)
ØØ 
]
ØØ 
[
∞∞ 	"
ProducesResponseType
∞∞	 
(
∞∞ 
StatusCodes
∞∞ )
.
∞∞) *
Status200OK
∞∞* 5
)
∞∞5 6
]
∞∞6 7
[
±± 	"
ProducesResponseType
±±	 
(
±± 
StatusCodes
±± )
.
±±) *!
Status400BadRequest
±±* =
)
±±= >
]
±±> ?
[
≤≤ 	"
ProducesResponseType
≤≤	 
(
≤≤ 
StatusCodes
≤≤ )
.
≤≤) *#
Status401Unauthorized
≤≤* ?
)
≤≤? @
]
≤≤@ A
[
≥≥ 	"
ProducesResponseType
≥≥	 
(
≥≥ 
StatusCodes
≥≥ )
.
≥≥) *
Status404NotFound
≥≥* ;
)
≥≥; <
]
≥≥< =
[
¥¥ 	"
ProducesResponseType
¥¥	 
(
¥¥ 
StatusCodes
¥¥ )
.
¥¥) **
Status500InternalServerError
¥¥* F
)
¥¥F G
]
¥¥G H
public
µµ 
async
µµ 
Task
µµ 
<
µµ 
IActionResult
µµ '
>
µµ' (
DeleteContacto
µµ) 7
(
µµ7 8
int
µµ8 ;
id
µµ< >
)
µµ> ?
{
∂∂ 	
try
∑∑ 
{
∏∏ 
if
ππ 
(
ππ 
User
ππ 
.
ππ 
Identity
ππ !
==
ππ" $
null
ππ% )
||
ππ* ,
!
ππ- .
User
ππ. 2
.
ππ2 3
Identity
ππ3 ;
.
ππ; <
IsAuthenticated
ππ< K
)
ππK L
{
∫∫ 
return
ªª 

StatusCode
ªª %
(
ªª% &
$num
ªª& )
,
ªª) *
$str
ªª+ I
)
ªªI J
;
ªªJ K
}
ºº 
if
ææ 
(
ææ 
id
ææ 
<=
ææ 
$num
ææ 
)
ææ 
{
øø 
return
¿¿ 

StatusCode
¿¿ %
(
¿¿% &
$num
¿¿& )
,
¿¿) *
$str
¿¿+ R
)
¿¿R S
;
¿¿S T
}
¡¡ 
var
√√ 
exist
√√ 
=
√√ 
await
√√ !!
_contactorepository
√√" 5
.
√√5 6
GetContactoById
√√6 E
(
√√E F
id
√√F H
)
√√H I
;
√√I J
if
≈≈ 
(
≈≈ 
exist
≈≈ 
==
≈≈ 
null
≈≈ !
)
≈≈! "
{
∆∆ 
return
«« 

StatusCode
«« %
(
««% &
$num
»» 
,
»» 
$"
…… 
$str
…… H
{
……H I
id
……I K
}
……K L
$str
……L W
"
……W X
)
   
;
   
}
ÀÀ 
var
ÕÕ 
response
ÕÕ 
=
ÕÕ 
await
ÕÕ $!
_contactorepository
ÕÕ% 8
.
ÕÕ8 9
DeleteContacto
ÕÕ9 G
(
ÕÕG H
exist
ÕÕH M
)
ÕÕM N
;
ÕÕN O
return
œœ 

StatusCode
œœ !
(
œœ! "
$num
œœ" %
,
œœ% &
response
œœ' /
)
œœ/ 0
;
œœ0 1
}
–– 
catch
—— 
(
—— 
	Exception
—— 
)
—— 
{
““ 
return
”” 

StatusCode
”” !
(
””! "
$num
””" %
,
””% &
$str
””' I
)
””I J
;
””J K
}
‘‘ 
}
’’ 	
}
÷÷ 
}◊◊ œz
bC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Controllers\CiudadController.cs
	namespace 	
rever
 
. 
Controllers 
{ 
[ 
Route 

(
 
$str 
) 
] 
[ 
ApiController 
] 
[ 
	Authorize 
( 
Roles 
= 
$str 
) 
] 
public 

class 
CiudadController !
:" #
ControllerBase$ 2
{ 
private 
readonly 
ICiudadRepository *
_ciudadrepository+ <
;< =
public 
CiudadController 
(  
ICiudadRepository  1

repository2 <
)< =
{ 	
_ciudadrepository 
= 

repository  *
;* +
} 	
[ 	
HttpGet	 
] 
[ 	
AllowAnonymous	 
] 
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *
Status200OK* 5
)5 6
]6 7
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *!
Status401Unauthorized* ?
)? @
]@ A
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *
Status404NotFound* ;
); <
]< =
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *(
Status500InternalServerError* F
)F G
]G H
public 
async 
Task 
< 
IActionResult '
>' (
ListarCiudad) 5
(5 6
)6 7
{   	
try!! 
{"" 
var## 
response## 
=## 
await## $
_ciudadrepository##% 6
.##6 7
	GetCiudad##7 @
(##@ A
)##A B
;##B C
if$$ 
($$ 
response$$ 
==$$ 
null$$  $
)$$$ %
{%% 
return&& 

StatusCode&& %
(&&% &
$num&&& )
,&&) *
$str&&+ Y
)&&Y Z
;&&Z [
}'' 
return(( 

StatusCode(( !
(((! "
$num((" %
,((% &
response((' /
)((/ 0
;((0 1
})) 
catch** 
(** 
	Exception** 
ex** 
)**  
{++ 
return,, 

StatusCode,, !
(,,! "
$num,," %
,,,% &
$",,' )
$str,,) <
{,,< =
ex,,= ?
.,,? @
Message,,@ G
},,G H
",,H I
),,I J
;,,J K
}-- 
}.. 	
[00 	
HttpGet00	 
(00 
$str00 
)00 
]00 
[11 	
AllowAnonymous11	 
]11 
[22 	 
ProducesResponseType22	 
(22 
StatusCodes22 )
.22) *
Status200OK22* 5
)225 6
]226 7
[33 	 
ProducesResponseType33	 
(33 
StatusCodes33 )
.33) *
Status400BadRequest33* =
)33= >
]33> ?
[44 	 
ProducesResponseType44	 
(44 
StatusCodes44 )
.44) *!
Status401Unauthorized44* ?
)44? @
]44@ A
[55 	 
ProducesResponseType55	 
(55 
StatusCodes55 )
.55) *
Status404NotFound55* ;
)55; <
]55< =
[66 	 
ProducesResponseType66	 
(66 
StatusCodes66 )
.66) *(
Status500InternalServerError66* F
)66F G
]66G H
public77 
async77 
Task77 
<77 
IActionResult77 '
>77' (
ObtenerCiudad77) 6
(776 7
int777 :
id77; =
)77= >
{88 	
try99 
{:: 
if;; 
(;; 
id;; 
<=;; 
$num;; 
);; 
{<< 
return== 

StatusCode== %
(==% &
$num==& )
,==) *
$str==+ S
)==S T
;==T U
}>> 
var@@ 
response@@ 
=@@ 
await@@ $
_ciudadrepository@@% 6
.@@6 7
GetCiudadById@@7 D
(@@D E
id@@E G
)@@G H
;@@H I
ifAA 
(AA 
responseAA 
==AA 
nullAA  $
)AA$ %
{BB 
returnCC 

StatusCodeCC %
(CC% &
$numCC& )
,CC) *
$"CC+ -
$strCC- R
{CCR S
idCCS U
}CCU V
$strCCV W
"CCW X
)CCX Y
;CCY Z
}DD 
returnEE 

StatusCodeEE !
(EE! "
$numEE" %
,EE% &
responseEE' /
)EE/ 0
;EE0 1
}FF 
catchGG 
(GG 
	ExceptionGG 
exGG 
)GG  
{HH 
returnII 

StatusCodeII !
(II! "
$numII" %
,II% &
$"II' )
$strII) <
{II< =
exII= ?
.II? @
MessageII@ G
}IIG H
"IIH I
)III J
;IIJ K
}JJ 
}KK 	
[MM 	
HttpPostMM	 
]MM 
[NN 	 
ProducesResponseTypeNN	 
(NN 
StatusCodesNN )
.NN) *
Status200OKNN* 5
)NN5 6
]NN6 7
[OO 	 
ProducesResponseTypeOO	 
(OO 
StatusCodesOO )
.OO) *
Status400BadRequestOO* =
)OO= >
]OO> ?
[PP 	 
ProducesResponseTypePP	 
(PP 
StatusCodesPP )
.PP) *!
Status401UnauthorizedPP* ?
)PP? @
]PP@ A
[QQ 	 
ProducesResponseTypeQQ	 
(QQ 
StatusCodesQQ )
.QQ) *(
Status500InternalServerErrorQQ* F
)QQF G
]QQG H
publicRR 
asyncRR 
TaskRR 
<RR 
IActionResultRR '
>RR' (
CrearCiudadRR) 4
(RR4 5
[RR5 6
FromBodyRR6 >
]RR> ?
CiudadRR@ F
ciudadRRG M
)RRM N
{SS 	
tryTT 
{UU 
ifVV 
(VV 
UserVV 
.VV 
IdentityVV !
==VV" $
nullVV% )
||VV* ,
!VV- .
UserVV. 2
.VV2 3
IdentityVV3 ;
.VV; <
IsAuthenticatedVV< K
)VVK L
{WW 
returnXX 

StatusCodeXX %
(XX% &
$numXX& )
,XX) *
$strXX+ I
)XXI J
;XXJ K
}YY 
if[[ 
([[ 
ciudad[[ 
==[[ 
null[[ "
)[[" #
{\\ 
return]] 

StatusCode]] %
(]]% &
$num]]& )
,]]) *
$str]]+ ]
)]]] ^
;]]^ _
}^^ 
var`` 
response`` 
=`` 
await`` $
_ciudadrepository``% 6
.``6 7

PostCiudad``7 A
(``A B
ciudad``B H
)``H I
;``I J
ifaa 
(aa 
responseaa 
==aa 
nullaa  $
)aa$ %
{bb 
returncc 

StatusCodecc %
(cc% &
$numcc& )
,cc) *
$strcc+ ]
)cc] ^
;cc^ _
}dd 
returnee 

StatusCodeee !
(ee! "
$numee" %
,ee% &
responseee' /
)ee/ 0
;ee0 1
}ff 
catchgg 
(gg 
	Exceptiongg 
exgg 
)gg  
{hh 
returnii 

StatusCodeii !
(ii! "
$numii" %
,ii% &
$"ii' )
$strii) <
{ii< =
exii= ?
.ii? @
Messageii@ G
}iiG H
"iiH I
)iiI J
;iiJ K
}jj 
}kk 	
[mm 	
HttpPutmm	 
]mm 
[nn 	 
ProducesResponseTypenn	 
(nn 
StatusCodesnn )
.nn) *
Status200OKnn* 5
)nn5 6
]nn6 7
[oo 	 
ProducesResponseTypeoo	 
(oo 
StatusCodesoo )
.oo) *
Status400BadRequestoo* =
)oo= >
]oo> ?
[pp 	 
ProducesResponseTypepp	 
(pp 
StatusCodespp )
.pp) *!
Status401Unauthorizedpp* ?
)pp? @
]pp@ A
[qq 	 
ProducesResponseTypeqq	 
(qq 
StatusCodesqq )
.qq) *
Status404NotFoundqq* ;
)qq; <
]qq< =
[rr 	 
ProducesResponseTyperr	 
(rr 
StatusCodesrr )
.rr) *(
Status500InternalServerErrorrr* F
)rrF G
]rrG H
publicss 
asyncss 
Taskss 
<ss 
IActionResultss '
>ss' (
ActualizarCiudadss) 9
(ss9 :
[ss: ;
FromBodyss; C
]ssC D
CiudadssE K
ciudadssL R
)ssR S
{tt 	
tryuu 
{vv 
ifww 
(ww 
Userww 
.ww 
Identityww !
==ww" $
nullww% )
||ww* ,
!ww- .
Userww. 2
.ww2 3
Identityww3 ;
.ww; <
IsAuthenticatedww< K
)wwK L
{xx 
returnyy 

StatusCodeyy %
(yy% &
$numyy& )
,yy) *
$stryy+ I
)yyI J
;yyJ K
}zz 
if|| 
(|| 
ciudad|| 
==|| 
null|| "
||||# %
ciudad||& ,
.||, -
IdCiudad||- 5
<=||6 8
$num||9 :
)||: ;
{}} 
return~~ 

StatusCode~~ %
(~~% &
$num~~& )
,~~) *
$str~~+ c
)~~c d
;~~d e
} 
var
ÅÅ 
exist
ÅÅ 
=
ÅÅ 
await
ÅÅ !
_ciudadrepository
ÅÅ" 3
.
ÅÅ3 4
GetCiudadById
ÅÅ4 A
(
ÅÅA B
ciudad
ÅÅB H
.
ÅÅH I
IdCiudad
ÅÅI Q
)
ÅÅQ R
;
ÅÅR S
if
ÇÇ 
(
ÇÇ 
exist
ÇÇ 
==
ÇÇ 
null
ÇÇ !
)
ÇÇ! "
{
ÉÉ 
return
ÑÑ 

StatusCode
ÑÑ %
(
ÑÑ% &
$num
ÑÑ& )
,
ÑÑ) *
$"
ÑÑ+ -
$str
ÑÑ- [
{
ÑÑ[ \
ciudad
ÑÑ\ b
.
ÑÑb c
IdCiudad
ÑÑc k
}
ÑÑk l
$str
ÑÑl w
"
ÑÑw x
)
ÑÑx y
;
ÑÑy z
}
ÖÖ 
exist
áá 
.
áá 
Nombre
áá 
=
áá 
ciudad
áá %
.
áá% &
Nombre
áá& ,
;
áá, -
var
àà 
response
àà 
=
àà 
await
àà $
_ciudadrepository
àà% 6
.
àà6 7
	PutCiudad
àà7 @
(
àà@ A
exist
ààA F
)
ààF G
;
ààG H
return
ââ 

StatusCode
ââ !
(
ââ! "
$num
ââ" %
,
ââ% &
response
ââ' /
)
ââ/ 0
;
ââ0 1
}
ãã 
catch
åå 
(
åå 
	Exception
åå 
ex
åå 
)
åå  
{
çç 
return
éé 

StatusCode
éé !
(
éé! "
$num
éé" %
,
éé% &
$"
éé' )
$str
éé) <
{
éé< =
ex
éé= ?
.
éé? @
Message
éé@ G
}
ééG H
"
ééH I
)
ééI J
;
ééJ K
}
èè 
}
êê 	
[
íí 	

HttpDelete
íí	 
(
íí 
$str
íí 
)
íí 
]
íí 
[
ìì 	"
ProducesResponseType
ìì	 
(
ìì 
StatusCodes
ìì )
.
ìì) *
Status200OK
ìì* 5
)
ìì5 6
]
ìì6 7
[
îî 	"
ProducesResponseType
îî	 
(
îî 
StatusCodes
îî )
.
îî) *!
Status400BadRequest
îî* =
)
îî= >
]
îî> ?
[
ïï 	"
ProducesResponseType
ïï	 
(
ïï 
StatusCodes
ïï )
.
ïï) *#
Status401Unauthorized
ïï* ?
)
ïï? @
]
ïï@ A
[
ññ 	"
ProducesResponseType
ññ	 
(
ññ 
StatusCodes
ññ )
.
ññ) *
Status404NotFound
ññ* ;
)
ññ; <
]
ññ< =
[
óó 	"
ProducesResponseType
óó	 
(
óó 
StatusCodes
óó )
.
óó) **
Status500InternalServerError
óó* F
)
óóF G
]
óóG H
public
òò 
async
òò 
Task
òò 
<
òò 
IActionResult
òò '
>
òò' (
EliminarCiudad
òò) 7
(
òò7 8
int
òò8 ;
id
òò< >
)
òò> ?
{
ôô 	
try
öö 
{
õõ 
if
úú 
(
úú 
User
úú 
.
úú 
Identity
úú !
==
úú" $
null
úú% )
||
úú* ,
!
úú- .
User
úú. 2
.
úú2 3
Identity
úú3 ;
.
úú; <
IsAuthenticated
úú< K
)
úúK L
{
ùù 
return
ûû 

StatusCode
ûû %
(
ûû% &
$num
ûû& )
,
ûû) *
$str
ûû+ I
)
ûûI J
;
ûûJ K
}
üü 
if
°° 
(
°° 
id
°° 
<=
°° 
$num
°° 
)
°° 
{
¢¢ 
return
££ 

StatusCode
££ %
(
££% &
$num
££& )
,
££) *
$str
££+ a
)
££a b
;
££b c
}
§§ 
var
¶¶ 
exist
¶¶ 
=
¶¶ 
await
¶¶ !
_ciudadrepository
¶¶" 3
.
¶¶3 4
GetCiudadById
¶¶4 A
(
¶¶A B
id
¶¶B D
)
¶¶D E
;
¶¶E F
if
ßß 
(
ßß 
exist
ßß 
==
ßß 
null
ßß !
)
ßß! "
{
®® 
return
©© 

StatusCode
©© %
(
©©% &
$num
©©& )
,
©©) *
$"
©©+ -
$str
©©- Y
{
©©Y Z
id
©©Z \
}
©©\ ]
$str
©©] h
"
©©h i
)
©©i j
;
©©j k
}
™™ 
var
¨¨ 
response
¨¨ 
=
¨¨ 
await
¨¨ $
_ciudadrepository
¨¨% 6
.
¨¨6 7
DeleteCiudad
¨¨7 C
(
¨¨C D
exist
¨¨D I
)
¨¨I J
;
¨¨J K
return
≠≠ 

StatusCode
≠≠ !
(
≠≠! "
$num
≠≠" %
,
≠≠% &
response
≠≠' /
)
≠≠/ 0
;
≠≠0 1
}
ÆÆ 
catch
ØØ 
(
ØØ 
	Exception
ØØ 
ex
ØØ 
)
ØØ  
{
∞∞ 
return
±± 

StatusCode
±± !
(
±±! "
$num
±±" %
,
±±% &
$"
±±' )
$str
±±) <
{
±±< =
ex
±±= ?
.
±±? @
Message
±±@ G
}
±±G H
"
±±H I
)
±±I J
;
±±J K
}
≤≤ 
}
≥≥ 	
}
¥¥ 
}µµ ö}
jC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Controllers\CaracteristicaController.cs
	namespace

 	
rever


 
.

 
Controllers

 
{ 
[ 
Route 

(
 
$str 
) 
] 
[ 
ApiController 
] 
[ 
	Authorize 
( 
Roles 
= 
$str 
) 
] 
public 

class $
CaracteristicaController )
:* +
ControllerBase, :
{ 
private 
readonly %
ICaracteristicaRepository 2%
_caracteristicarepository3 L
;L M
public $
CaracteristicaController '
(' (%
ICaracteristicaRepository( A

repositoryB L
)L M
{ 	%
_caracteristicarepository %
=& '

repository( 2
;2 3
} 	
[ 	
HttpGet	 
] 
[ 	
AllowAnonymous	 
] 
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *
Status200OK* 5
)5 6
]6 7
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *!
Status401Unauthorized* ?
)? @
]@ A
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *
Status404NotFound* ;
); <
]< =
[ 	 
ProducesResponseType	 
( 
StatusCodes )
.) *(
Status500InternalServerError* F
)F G
]G H
public 
async 
Task 
< 
IActionResult '
>' ( 
ListarCaracteristica) =
(= >
)> ?
{ 	
try   
{!! 
var"" 
response"" 
="" 
await"" $%
_caracteristicarepository""% >
.""> ?
GetCaracteristica""? P
(""P Q
)""Q R
;""R S
if## 
(## 
response## 
==## 
null##  $
)##$ %
{$$ 
return%% 

StatusCode%% %
(%%% &
$num%%& )
,%%) *
$str%%+ `
)%%` a
;%%a b
}&& 
return'' 

StatusCode'' !
(''! "
$num''" %
,''% &
response''' /
)''/ 0
;''0 1
}(( 
catch)) 
()) 
	Exception)) 
ex)) 
)))  
{** 
return++ 

StatusCode++ !
(++! "
$num++" %
,++% &
$"++' )
$str++) <
{++< =
ex++= ?
.++? @
Message++@ G
}++G H
"++H I
)++I J
;++J K
},, 
}-- 	
[// 	
HttpGet//	 
(// 
$str// 
)// 
]// 
[00 	
AllowAnonymous00	 
]00 
[11 	 
ProducesResponseType11	 
(11 
StatusCodes11 )
.11) *
Status200OK11* 5
)115 6
]116 7
[22 	 
ProducesResponseType22	 
(22 
StatusCodes22 )
.22) *
Status400BadRequest22* =
)22= >
]22> ?
[33 	 
ProducesResponseType33	 
(33 
StatusCodes33 )
.33) *!
Status401Unauthorized33* ?
)33? @
]33@ A
[44 	 
ProducesResponseType44	 
(44 
StatusCodes44 )
.44) *
Status404NotFound44* ;
)44; <
]44< =
[55 	 
ProducesResponseType55	 
(55 
StatusCodes55 )
.55) *(
Status500InternalServerError55* F
)55F G
]55G H
public66 
async66 
Task66 
<66 
IActionResult66 '
>66' (!
ObtenerCaracteristica66) >
(66> ?
int66? B
id66C E
)66E F
{77 	
try88 
{99 
if:: 
(:: 
id:: 
<=:: 
$num:: 
):: 
{;; 
return<< 

StatusCode<< %
(<<% &
$num<<& )
,<<) *
$str<<+ S
)<<S T
;<<T U
}== 
var?? 
response?? 
=?? 
await?? $%
_caracteristicarepository??% >
.??> ?!
GetCaracteristicaById??? T
(??T U
id??U W
)??W X
;??X Y
if@@ 
(@@ 
response@@ 
==@@ 
null@@  $
)@@$ %
{AA 
returnBB 

StatusCodeBB %
(BB% &
$numBB& )
,BB) *
$"BB+ -
$strBB- Z
{BBZ [
idBB[ ]
}BB] ^
$strBB^ _
"BB_ `
)BB` a
;BBa b
}CC 
returnDD 

StatusCodeDD !
(DD! "
$numDD" %
,DD% &
responseDD' /
)DD/ 0
;DD0 1
}EE 
catchFF 
(FF 
	ExceptionFF 
exFF 
)FF  
{GG 
returnHH 

StatusCodeHH !
(HH! "
$numHH" %
,HH% &
$"HH' )
$strHH) <
{HH< =
exHH= ?
.HH? @
MessageHH@ G
}HHG H
"HHH I
)HHI J
;HHJ K
}II 
}JJ 	
[LL 	
HttpPostLL	 
]LL 
[MM 	 
ProducesResponseTypeMM	 
(MM 
StatusCodesMM )
.MM) *
Status200OKMM* 5
)MM5 6
]MM6 7
[NN 	 
ProducesResponseTypeNN	 
(NN 
StatusCodesNN )
.NN) *
Status400BadRequestNN* =
)NN= >
]NN> ?
[OO 	 
ProducesResponseTypeOO	 
(OO 
StatusCodesOO )
.OO) *!
Status401UnauthorizedOO* ?
)OO? @
]OO@ A
[PP 	 
ProducesResponseTypePP	 
(PP 
StatusCodesPP )
.PP) *(
Status500InternalServerErrorPP* F
)PPF G
]PPG H
publicQQ 
asyncQQ 
TaskQQ 
<QQ 
IActionResultQQ '
>QQ' (
CrearCaracteristicaQQ) <
(QQ< =
[QQ= >
FromBodyQQ> F
]QQF G
CaracteristicaQQH V
caracteristicaQQW e
)QQe f
{RR 	
trySS 
{TT 
ifUU 
(UU 
UserUU 
.UU 
IdentityUU !
==UU" $
nullUU% )
||UU* ,
!UU- .
UserUU. 2
.UU2 3
IdentityUU3 ;
.UU; <
IsAuthenticatedUU< K
)UUK L
{VV 
returnWW 

StatusCodeWW %
(WW% &
$numWW& )
,WW) *
$strWW+ I
)WWI J
;WWJ K
}XX 
ifZZ 
(ZZ 
caracteristicaZZ "
==ZZ# %
nullZZ& *
)ZZ* +
{[[ 
return\\ 

StatusCode\\ %
(\\% &
$num\\& )
,\\) *
$str\\+ e
)\\e f
;\\f g
}]] 
var__ 
response__ 
=__ 
await__ $%
_caracteristicarepository__% >
.__> ?
PostCaracteristica__? Q
(__Q R
caracteristica__R `
)__` a
;__a b
if`` 
(`` 
response`` 
==`` 
null``  $
)``$ %
{aa 
returnbb 

StatusCodebb %
(bb% &
$numbb& )
,bb) *
$strbb+ ]
)bb] ^
;bb^ _
}cc 
returndd 

StatusCodedd !
(dd! "
$numdd" %
,dd% &
responsedd' /
)dd/ 0
;dd0 1
}ee 
catchff 
(ff 
	Exceptionff 
exff 
)ff  
{gg 
returnhh 

StatusCodehh !
(hh! "
$numhh" %
,hh% &
$"hh' )
$strhh) <
{hh< =
exhh= ?
.hh? @
Messagehh@ G
}hhG H
"hhH I
)hhI J
;hhJ K
}ii 
}jj 	
[ll 	
HttpPutll	 
]ll 
[mm 	 
ProducesResponseTypemm	 
(mm 
StatusCodesmm )
.mm) *
Status200OKmm* 5
)mm5 6
]mm6 7
[nn 	 
ProducesResponseTypenn	 
(nn 
StatusCodesnn )
.nn) *
Status400BadRequestnn* =
)nn= >
]nn> ?
[oo 	 
ProducesResponseTypeoo	 
(oo 
StatusCodesoo )
.oo) *!
Status401Unauthorizedoo* ?
)oo? @
]oo@ A
[pp 	 
ProducesResponseTypepp	 
(pp 
StatusCodespp )
.pp) *
Status404NotFoundpp* ;
)pp; <
]pp< =
[qq 	 
ProducesResponseTypeqq	 
(qq 
StatusCodesqq )
.qq) *(
Status500InternalServerErrorqq* F
)qqF G
]qqG H
publicrr 
asyncrr 
Taskrr 
<rr 
IActionResultrr '
>rr' ($
ActualizarCaracteristicarr) A
(rrA B
[rrB C
FromBodyrrC K
]rrK L
CaracteristicarrM [
caracteristicarr\ j
)rrj k
{ss 	
trytt 
{uu 
ifvv 
(vv 
Uservv 
.vv 
Identityvv !
==vv" $
nullvv% )
||vv* ,
!vv- .
Uservv. 2
.vv2 3
Identityvv3 ;
.vv; <
IsAuthenticatedvv< K
)vvK L
{ww 
returnxx 

StatusCodexx %
(xx% &
$numxx& )
,xx) *
$strxx+ I
)xxI J
;xxJ K
}yy 
if{{ 
({{ 
caracteristica{{ "
=={{# %
null{{& *
||{{+ -
caracteristica{{. <
.{{< =
IdCaracteristica{{= M
<={{N P
$num{{Q R
){{R S
{|| 
return}} 

StatusCode}} %
(}}% &
$num}}& )
,}}) *
$str}}+ c
)}}c d
;}}d e
}~~ 
var
ÄÄ 
exist
ÄÄ 
=
ÄÄ 
await
ÄÄ !'
_caracteristicarepository
ÄÄ" ;
.
ÄÄ; <#
GetCaracteristicaById
ÄÄ< Q
(
ÄÄQ R
caracteristica
ÄÄR `
.
ÄÄ` a
IdCaracteristica
ÄÄa q
)
ÄÄq r
;
ÄÄr s
if
ÅÅ 
(
ÅÅ 
exist
ÅÅ 
==
ÅÅ 
null
ÅÅ !
)
ÅÅ! "
{
ÇÇ 
return
ÉÉ 

StatusCode
ÉÉ %
(
ÉÉ% &
$num
ÉÉ& )
,
ÉÉ) *
$"
ÉÉ+ -
$str
ÉÉ- c
{
ÉÉc d
caracteristica
ÉÉd r
.
ÉÉr s
IdCaracteristicaÉÉs É
}ÉÉÉ Ñ
$strÉÉÑ è
"ÉÉè ê
)ÉÉê ë
;ÉÉë í
}
ÑÑ 
exist
ÜÜ 
.
ÜÜ 
Nombre
ÜÜ 
=
ÜÜ 
caracteristica
ÜÜ -
.
ÜÜ- .
Nombre
ÜÜ. 4
;
ÜÜ4 5
var
áá 
response
áá 
=
áá 
await
áá $'
_caracteristicarepository
áá% >
.
áá> ?
PutCaracteristica
áá? P
(
ááP Q
exist
ááQ V
)
ááV W
;
ááW X
return
àà 

StatusCode
àà !
(
àà! "
$num
àà" %
,
àà% &
response
àà' /
)
àà/ 0
;
àà0 1
}
ââ 
catch
ää 
(
ää 
	Exception
ää 
ex
ää 
)
ää  
{
ãã 
return
åå 

StatusCode
åå !
(
åå! "
$num
åå" %
,
åå% &
$"
åå' )
$str
åå) <
{
åå< =
ex
åå= ?
.
åå? @
Message
åå@ G
}
ååG H
"
ååH I
)
ååI J
;
ååJ K
}
çç 
}
éé 	
[
êê 	

HttpDelete
êê	 
(
êê 
$str
êê 
)
êê 
]
êê 
[
ëë 	"
ProducesResponseType
ëë	 
(
ëë 
StatusCodes
ëë )
.
ëë) *
Status200OK
ëë* 5
)
ëë5 6
]
ëë6 7
[
íí 	"
ProducesResponseType
íí	 
(
íí 
StatusCodes
íí )
.
íí) *!
Status400BadRequest
íí* =
)
íí= >
]
íí> ?
[
ìì 	"
ProducesResponseType
ìì	 
(
ìì 
StatusCodes
ìì )
.
ìì) *#
Status401Unauthorized
ìì* ?
)
ìì? @
]
ìì@ A
[
îî 	"
ProducesResponseType
îî	 
(
îî 
StatusCodes
îî )
.
îî) *
Status404NotFound
îî* ;
)
îî; <
]
îî< =
[
ïï 	"
ProducesResponseType
ïï	 
(
ïï 
StatusCodes
ïï )
.
ïï) **
Status500InternalServerError
ïï* F
)
ïïF G
]
ïïG H
public
ññ 
async
ññ 
Task
ññ 
<
ññ 
IActionResult
ññ '
>
ññ' ($
EliminarCaracteristica
ññ) ?
(
ññ? @
int
ññ@ C
id
ññD F
)
ññF G
{
óó 	
try
òò 
{
ôô 
if
öö 
(
öö 
User
öö 
.
öö 
Identity
öö !
==
öö" $
null
öö% )
||
öö* ,
!
öö- .
User
öö. 2
.
öö2 3
Identity
öö3 ;
.
öö; <
IsAuthenticated
öö< K
)
ööK L
{
õõ 
return
úú 

StatusCode
úú %
(
úú% &
$num
úú& )
,
úú) *
$str
úú+ I
)
úúI J
;
úúJ K
}
ùù 
if
üü 
(
üü 
id
üü 
<=
üü 
$num
üü 
)
üü 
{
†† 
return
°° 

StatusCode
°° %
(
°°% &
$num
°°& )
,
°°) *
$str
°°+ a
)
°°a b
;
°°b c
}
¢¢ 
var
§§ 
exist
§§ 
=
§§ 
await
§§ !'
_caracteristicarepository
§§" ;
.
§§; <#
GetCaracteristicaById
§§< Q
(
§§Q R
id
§§R T
)
§§T U
;
§§U V
if
•• 
(
•• 
exist
•• 
==
•• 
null
•• !
)
••! "
{
¶¶ 
return
ßß 

StatusCode
ßß %
(
ßß% &
$num
ßß& )
,
ßß) *
$"
ßß+ -
$str
ßß- a
{
ßßa b
id
ßßb d
}
ßßd e
$str
ßße p
"
ßßp q
)
ßßq r
;
ßßr s
}
®® 
var
™™ 
response
™™ 
=
™™ 
await
™™ $'
_caracteristicarepository
™™% >
.
™™> ?"
DeleteCaracteristica
™™? S
(
™™S T
exist
™™T Y
)
™™Y Z
;
™™Z [
return
´´ 

StatusCode
´´ !
(
´´! "
$num
´´" %
,
´´% &
response
´´' /
)
´´/ 0
;
´´0 1
}
¨¨ 
catch
≠≠ 
(
≠≠ 
	Exception
≠≠ 
ex
≠≠ 
)
≠≠  
{
ÆÆ 
return
ØØ 

StatusCode
ØØ !
(
ØØ! "
$num
ØØ" %
,
ØØ% &
$"
ØØ' )
$str
ØØ) <
{
ØØ< =
ex
ØØ= ?
.
ØØ? @
Message
ØØ@ G
}
ØØG H
"
ØØH I
)
ØØI J
;
ØØJ K
}
∞∞ 
}
±± 	
}
≤≤ 
}≥≥ ßn
`C:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\Controllers\AuthController.cs
	namespace 	
rever
 
. 
Controllers 
{ 
[ 
Route 

(
 
$str 
) 
] 
[ 
ApiController 
] 
[ 
AllowAnonymous 
] 
public 

class 
AuthController 
:  !
ControllerBase" 0
{ 
private 
readonly 
IConfiguration '
_configuration( 6
;6 7
private 
readonly 
IUsuarioRepository +
_usuarioRepository, >
;> ?
private 
readonly 
IEmailService &
_emailService' 4
;4 5
public 
AuthController 
( 
IConfiguration ,
configuration- :
,: ;
IUsuarioRepository< N
usuarioRepositoryO `
,` a
IEmailServiceb o
emailServicep |
)| }
{ 	
_configuration 
= 
configuration *
;* +
_usuarioRepository 
=  
usuarioRepository! 2
;2 3
_emailService 
= 
emailService (
;( )
} 	
[ 	
HttpPost	 
( 
$str 
) 
] 
public 
async 
Task 
< 
IActionResult '
>' (
Login) .
(. /
[/ 0
FromBody0 8
]8 9
Login: ?
login@ E
)E F
{   	
if!! 
(!! 
login!! 
==!! 
null!! 
||!!  
string!!! '
.!!' (
IsNullOrWhiteSpace!!( :
(!!: ;
login!!; @
.!!@ A
Correo!!A G
)!!G H
||!!I K
string!!L R
.!!R S
IsNullOrWhiteSpace!!S e
(!!e f
login!!f k
.!!k l
Contrase√±a!!l v
)!!v w
)!!w x
{"" 
return## 

BadRequest## !
(##! "
$str##" :
)##: ;
;##; <
}$$ 
var&& 
usuario&& 
=&& 
await&& 
_usuarioRepository&&  2
.&&2 3"
GetByEmailWithRolAsync&&3 I
(&&I J
login&&J O
.&&O P
Correo&&P V
)&&V W
;&&W X
if'' 
('' 
usuario'' 
=='' 
null'' 
)''  
{(( 
return)) 
Unauthorized)) #
())# $
$str))$ <
)))< =
;))= >
}** 
bool,, 
passwordValida,, 
=,,  !
BCrypt,," (
.,,( )
Net,,) ,
.,,, -
BCrypt,,- 3
.,,3 4
Verify,,4 :
(,,: ;
login,,; @
.,,@ A
Contrase√±a,,A K
,,,K L
usuario,,M T
.,,T U
Contrase√±a,,U _
),,_ `
;,,` a
if-- 
(-- 
!-- 
passwordValida-- 
)--  
{.. 
return// 
Unauthorized// #
(//# $
$str//$ <
)//< =
;//= >
}00 
if22 
(22 
!22 
usuario22 
.22 

Confirmado22 #
)22# $
{33 
return44 
Unauthorized44 #
(44# $
$str44$ x
)44x y
;44y z
}55 
var88 
claims88 
=88 
new88 
List88 !
<88! "
Claim88" '
>88' (
{99 
new:: 
Claim:: 
(:: 

ClaimTypes:: $
.::$ %
NameIdentifier::% 3
,::3 4
usuario::5 <
.::< =
	IdUsuario::= F
.::F G
ToString::G O
(::O P
)::P Q
)::Q R
,::R S
new;; 
Claim;; 
(;; 

ClaimTypes;; $
.;;$ %
Name;;% )
,;;) *
usuario;;+ 2
.;;2 3
Correo;;3 9
);;9 :
,;;: ;
new<< 
Claim<< 
(<< 

ClaimTypes<< $
.<<$ %
Role<<% )
,<<) *
usuario<<+ 2
.<<2 3
Rol<<3 6
?<<6 7
.<<7 8
IdRol<<8 =
.<<= >
ToString<<> F
(<<F G
)<<G H
??<<I K
$str<<L O
)<<O P
,<<P Q
new== 
Claim== 
(== 
$str== "
,==" #
usuario==$ +
.==+ ,
Nombre==, 2
)==2 3
,==3 4
}>> 
;>> 
var@@ 
keyBytes@@ 
=@@ 
Encoding@@ #
.@@# $
UTF8@@$ (
.@@( )
GetBytes@@) 1
(@@1 2
_configuration@@2 @
[@@@ A
$str@@A J
]@@J K
??@@L N
throw@@O T
new@@U X%
InvalidOperationException@@Y r
(@@r s
$str	@@s è
)
@@è ê
)
@@ê ë
;
@@ë í
varAA 
	secretKeyAA 
=AA 
newAA  
SymmetricSecurityKeyAA  4
(AA4 5
keyBytesAA5 =
)AA= >
;AA> ?
varBB 
signingCredentialsBB "
=BB# $
newBB% (
SigningCredentialsBB) ;
(BB; <
	secretKeyBB< E
,BBE F
SecurityAlgorithmsBBG Y
.BBY Z

HmacSha256BBZ d
)BBd e
;BBe f
varDD 
tokenOptionsDD 
=DD 
newDD "
JwtSecurityTokenDD# 3
(DD3 4
issuerEE 
:EE 
_configurationEE &
[EE& '
$strEE' 3
]EE3 4
,EE4 5
audienceFF 
:FF 
_configurationFF (
[FF( )
$strFF) 7
]FF7 8
,FF8 9
claimsGG 
:GG 
claimsGG 
,GG 
expiresHH 
:HH 
DateTimeHH !
.HH! "
UtcNowHH" (
.HH( )

AddMinutesHH) 3
(HH3 4
ConvertHH4 ;
.HH; <
ToDoubleHH< D
(HHD E
_configurationHHE S
[HHS T
$strHHT k
]HHk l
??HHm o
$strHHp t
)HHt u
)HHu v
,HHv w
signingCredentialsII "
:II" #
signingCredentialsII$ 6
)JJ 
;JJ 
varLL 
tokenStringLL 
=LL 
newLL !#
JwtSecurityTokenHandlerLL" 9
(LL9 :
)LL: ;
.LL; <

WriteTokenLL< F
(LLF G
tokenOptionsLLG S
)LLS T
;LLT U
returnNN 
OkNN 
(NN 
newNN 
{OO 
TokenPP 
=PP 
tokenStringPP #
,PP# $

ExpirationQQ 
=QQ 
tokenOptionsQQ )
.QQ) *
ValidToQQ* 1
,QQ1 2
}RR 
)RR 
;RR 
}SS 	
[UU 	
HttpPostUU	 
(UU 
$strUU 
)UU 
]UU 
publicVV 
asyncVV 
TaskVV 
<VV 
IActionResultVV '
>VV' (
RegisterVV) 1
(VV1 2
[VV2 3
FromBodyVV3 ;
]VV; <
UsuarioVV= D
nuevoUsuarioVVE Q
)VVQ R
{WW 	
ifXX 
(XX 
nuevoUsuarioXX 
==XX 
nullXX  $
||XX% '
stringXX( .
.XX. /
IsNullOrWhiteSpaceXX/ A
(XXA B
nuevoUsuarioXXB N
.XXN O
CorreoXXO U
)XXU V
||XXW Y
stringXXZ `
.XX` a
IsNullOrWhiteSpaceXXa s
(XXs t
nuevoUsuario	XXt Ä
.
XXÄ Å
Contrase√±a
XXÅ ã
)
XXã å
)
XXå ç
{YY 
returnZZ 

BadRequestZZ !
(ZZ! "
$strZZ" ?
)ZZ? @
;ZZ@ A
}[[ 
var]] 
	existente]] 
=]] 
await]] !
_usuarioRepository]]" 4
.]]4 5"
GetByEmailWithRolAsync]]5 K
(]]K L
nuevoUsuario]]L X
.]]X Y
Correo]]Y _
)]]_ `
;]]` a
if^^ 
(^^ 
	existente^^ 
!=^^ 
null^^ !
)^^! "
{__ 
return`` 

BadRequest`` !
(``! "
$str``" I
)``I J
;``J K
}aa 
booldd 
esAdministradordd  
=dd! "
Userdd# '
.dd' (
EsAdministradordd( 7
(dd7 8
)dd8 9
;dd9 :
ifee 
(ee 
!ee 
esAdministradoree  
&&ee! #
(ee$ %
nuevoUsuarioee% 1
.ee1 2
IdRolee2 7
!=ee8 :
$numee; <
&&ee= ?
nuevoUsuarioee@ L
.eeL M
IdRoleeM R
!=eeS U
$numeeV W
)eeW X
)eeX Y
{ff 
returngg 

BadRequestgg !
(gg! "
$strgg" Z
)ggZ [
;gg[ \
}hh 
stringjj 
confirmationTokenjj $
=jj% &
Guidjj' +
.jj+ ,
NewGuidjj, 3
(jj3 4
)jj4 5
.jj5 6
ToStringjj6 >
(jj> ?
)jj? @
;jj@ A
nuevoUsuarioll 
.ll 
Contrase√±all #
=ll$ %
BCryptll& ,
.ll, -
Netll- 0
.ll0 1
BCryptll1 7
.ll7 8
HashPasswordll8 D
(llD E
nuevoUsuariollE Q
.llQ R
Contrase√±allR \
)ll\ ]
;ll] ^
nuevoUsuariomm 
.mm 

Confirmadomm #
=mm$ %
falsemm& +
;mm+ ,
nuevoUsuarionn 
.nn 
TokenConfirmacionnn *
=nn+ ,
confirmationTokennn- >
;nn> ?
varpp 
guardadopp 
=pp 
awaitpp  
_usuarioRepositorypp! 3
.pp3 4
PostUsuariopp4 ?
(pp? @
nuevoUsuariopp@ L
)ppL M
;ppM N
ifqq 
(qq 
!qq 
guardadoqq 
)qq 
{rr 
returnss 

StatusCodess !
(ss! "
$numss" %
,ss% &
$strss' K
)ssK L
;ssL M
}tt 
stringvv 
confirmationLinkvv #
=vv$ %
$"vv& (
{vv( )
Requestvv) 0
.vv0 1
Schemevv1 7
}vv7 8
$strvv8 ;
{vv; <
Requestvv< C
.vvC D
HostvvD H
}vvH I
$strvvI f
{vvf g
nuevoUsuariovvg s
.vvs t
Correovvt z
}vvz {
$str	vv{ Ç
{
vvÇ É
confirmationToken
vvÉ î
}
vvî ï
"
vvï ñ
;
vvñ ó
stringxx 
	emailBodyxx 
=xx 
$@"xx "
$strx{" 
{{{  
nuevoUsuario{{  ,
.{{, -
Nombre{{- 3
}{{3 4
$str{}4 
{}} 
confirmationLink}} .
}}}. /
$str	}Å/ 
"
ÅÅ 
;
ÅÅ 
await
ÉÉ 
_emailService
ÉÉ 
.
ÉÉ  
SendEmailAsync
ÉÉ  .
(
ÉÉ. /
nuevoUsuario
ÉÉ/ ;
.
ÉÉ; <
Correo
ÉÉ< B
,
ÉÉB C
$str
ÉÉD a
,
ÉÉa b
	emailBody
ÉÉc l
)
ÉÉl m
;
ÉÉm n
return
ÖÖ 
Ok
ÖÖ 
(
ÖÖ 
new
ÖÖ 
{
ÖÖ 
message
ÖÖ #
=
ÖÖ$ %
$str
ÖÖ& b
}
ÖÖc d
)
ÖÖd e
;
ÖÖe f
}
ÜÜ 	
[
àà 	
HttpGet
àà	 
(
àà 
$str
àà 
)
àà  
]
àà  !
public
ââ 
async
ââ 
Task
ââ 
<
ââ 
IActionResult
ââ '
>
ââ' (
ConfirmEmail
ââ) 5
(
ââ5 6
[
ââ6 7
	FromQuery
ââ7 @
]
ââ@ A
string
ââB H
email
ââI N
,
ââN O
[
ââP Q
	FromQuery
ââQ Z
]
ââZ [
string
ââ\ b
token
ââc h
)
ââh i
{
ää 	
var
ãã 
usuario
ãã 
=
ãã 
await
ãã  
_usuarioRepository
ãã  2
.
ãã2 3$
GetByEmailWithRolAsync
ãã3 I
(
ããI J
email
ããJ O
)
ããO P
;
ããP Q
const
éé 
string
éé 
urlBaseFrontend
éé (
=
éé) *
$str
éé+ P
;
ééP Q
if
êê 
(
êê 
usuario
êê 
==
êê 
null
êê 
||
êê  "
usuario
êê# *
.
êê* +
TokenConfirmacion
êê+ <
!=
êê= ?
token
êê@ E
)
êêE F
{
ëë 
return
íí 
Redirect
íí 
(
íí  
$"
íí  "
{
íí" #
urlBaseFrontend
íí# 2
}
íí2 3
$str
íí3 R
"
ííR S
)
ííS T
;
ííT U
}
ìì 
usuario
ïï 
.
ïï 

Confirmado
ïï 
=
ïï  
true
ïï! %
;
ïï% &
usuario
ññ 
.
ññ 
TokenConfirmacion
ññ %
=
ññ& '
null
ññ( ,
;
ññ, -
await
óó  
_usuarioRepository
óó $
.
óó$ %

PutUsuario
óó% /
(
óó/ 0
usuario
óó0 7
)
óó7 8
;
óó8 9
return
ôô 
Redirect
ôô 
(
ôô 
$"
ôô 
{
ôô 
urlBaseFrontend
ôô .
}
ôô. /
$str
ôô/ I
"
ôôI J
)
ôôJ K
;
ôôK L
}
öö 	
}
õõ 
}úú Çô
]C:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\contexto\Databaseserver.cs
	namespace 	
rever
 
. 
contexto 
{ 
public 

class 
DatabaseService  
:! "
	DbContext# ,
{ 
public 
DatabaseService 
( 
DbContextOptions /
</ 0
DatabaseService0 ?
>? @
optionsA H
)H I
:J K
baseL P
(P Q
optionsQ X
)X Y
{		 	
}

 	
public 
DbSet 
< 
Rol 
> 
Rol 
{ 
get  #
;# $
set% (
;( )
}* +
public 
DbSet 
< 
Usuario 
> 
Usuario %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
DbSet 
< 
Ciudad 
> 
Ciudad #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
DbSet 
< 
	Localidad 
> 
	Localidad  )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public 
DbSet 
< 
TipoInmueble !
>! "
TipoInmueble# /
{0 1
get2 5
;5 6
set7 :
;: ;
}< =
public 
DbSet 
< 
EstadoPublicacion &
>& '
EstadoPublicacion( 9
{: ;
get< ?
;? @
setA D
;D E
}F G
public 
DbSet 
< 
Inmueble 
> 
Inmueble '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
DbSet 
< 
Imagen 
> 
Imagen #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
DbSet 
< 
Caracteristica #
># $
Caracteristica% 3
{4 5
get6 9
;9 :
set; >
;> ?
}@ A
public 
DbSet 
< "
InmuebleCaracteristica +
>+ ,"
InmuebleCaracteristica- C
{D E
getF I
;I J
setK N
;N O
}P Q
public 
DbSet 
< 
Contacto 
> 
Contacto '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
DbSet 
< 
ModoTransaccion $
>$ %
ModoTransaccion& 5
{6 7
get8 ;
;; <
set= @
;@ A
}B C
	protected 
override 
void 
OnModelCreating  /
(/ 0
ModelBuilder0 <
modelBuilder= I
)I J
{ 	
base 
. 
OnModelCreating  
(  !
modelBuilder! -
)- .
;. /
EntityConfiguration 
(  
modelBuilder  ,
), -
;- .
} 	
private 
void 
EntityConfiguration (
(( )
ModelBuilder) 5
modelBuilder6 B
)B C
{ 	
modelBuilder!! 
.!! 
Entity!! 
<!!  
Rol!!  #
>!!# $
(!!$ %
entity!!% +
=>!!, .
{"" 
entity## 
.## 
ToTable## 
(## 
$str## &
)##& '
;##' (
entity$$ 
.$$ 
HasKey$$ 
($$ 
u$$ 
=>$$  "
u$$# $
.$$$ %
IdRol$$% *
)$$* +
;$$+ ,
entity%% 
.%% 
Property%% 
(%%  
u%%  !
=>%%" $
u%%% &
.%%& '
IdRol%%' ,
)%%, -
.%%- .
HasColumnName%%. ;
(%%; <
$str%%< D
)%%D E
;%%E F
entity&& 
.&& 
Property&& 
(&&  
u&&  !
=>&&" $
u&&% &
.&&& '
Nombre&&' -
)&&- .
.&&. /
HasColumnName&&/ <
(&&< =
$str&&= E
)&&E F
.&&F G
HasMaxLength&&G S
(&&S T
$num&&T W
)&&W X
;&&X Y
}'' 
)'' 
;'' 
modelBuilder** 
.** 
Entity** 
<**  
Usuario**  '
>**' (
(**( )
entity**) /
=>**0 2
{++ 
entity,, 
.,, 
ToTable,, 
(,, 
$str,, )
),,) *
;,,* +
entity-- 
.-- 
HasKey-- 
(-- 
u-- 
=>--  "
u--# $
.--$ %
	IdUsuario--% .
)--. /
;--/ 0
entity// 
.// 
Property// 
(//  
u//  !
=>//" $
u//% &
.//& '
	IdUsuario//' 0
)//0 1
.//1 2
HasColumnName//2 ?
(//? @
$str//@ L
)//L M
.//M N
ValueGeneratedOnAdd//N a
(//a b
)//b c
;//c d
entity00 
.00 
Property00 
(00  
u00  !
=>00" $
u00% &
.00& '
Nombre00' -
)00- .
.00. /
HasColumnName00/ <
(00< =
$str00= E
)00E F
.00F G
HasMaxLength00G S
(00S T
$num00T W
)00W X
;00X Y
entity11 
.11 
Property11 
(11  
u11  !
=>11" $
u11% &
.11& '
Correo11' -
)11- .
.11. /
HasColumnName11/ <
(11< =
$str11= E
)11E F
.11F G

IsRequired11G Q
(11Q R
)11R S
.11S T
HasMaxLength11T `
(11` a
$num11a d
)11d e
;11e f
entity22 
.22 
Property22 
(22  
u22  !
=>22" $
u22% &
.22& '
Contrase√±a22' 1
)221 2
.222 3
HasColumnName223 @
(22@ A
$str22A M
)22M N
.22N O

IsRequired22O Y
(22Y Z
)22Z [
.22[ \
HasMaxLength22\ h
(22h i
$num22i l
)22l m
;22m n
entity33 
.33 
Property33 
(33  
u33  !
=>33" $
u33% &
.33& '
Telefono33' /
)33/ 0
.330 1
HasColumnName331 >
(33> ?
$str33? I
)33I J
.33J K
HasMaxLength33K W
(33W X
$num33X Z
)33Z [
;33[ \
entity44 
.44 
Property44 
(44  
u44  !
=>44" $
u44% &
.44& '
IdRol44' ,
)44, -
.44- .
HasColumnName44. ;
(44; <
$str44< D
)44D E
;44E F
entity55 
.55 
Property55 
(55  
u55  !
=>55" $
u55% &
.55& '
FechaRegistro55' 4
)554 5
.555 6
HasColumnName556 C
(55C D
$str55D T
)55T U
.55U V
HasDefaultValueSql55V h
(55h i
$str55i |
)55| }
;55} ~
entity66 
.66 
Property66 
(66  
u66  !
=>66" $
u66% &
.66& '

Confirmado66' 1
)661 2
.662 3
HasColumnName663 @
(66@ A
$str66A M
)66M N
.66N O
HasDefaultValue66O ^
(66^ _
false66_ d
)66d e
;66e f
entity77 
.77 
Property77 
(77  
u77  !
=>77" $
u77% &
.77& '
TokenConfirmacion77' 8
)778 9
.779 :
HasColumnName77: G
(77G H
$str77H \
)77\ ]
;77] ^
entity99 
.99 
HasIndex99 
(99  
u99  !
=>99" $
u99% &
.99& '
Correo99' -
)99- .
.99. /
IsUnique99/ 7
(997 8
)998 9
;999 :
entity:: 
.:: 
HasOne:: 
(:: 
u:: 
=>::  "
u::# $
.::$ %
Rol::% (
)::( )
.::) *
WithMany::* 2
(::2 3
)::3 4
.::4 5
HasForeignKey::5 B
(::B C
u::C D
=>::E G
u::H I
.::I J
IdRol::J O
)::O P
;::P Q
};; 
);; 
;;; 
modelBuilder>> 
.>> 
Entity>> 
<>>  
Ciudad>>  &
>>>& '
(>>' (
entity>>( .
=>>>/ 1
{?? 
entity@@ 
.@@ 
ToTable@@ 
(@@ 
$str@@ )
)@@) *
;@@* +
entityAA 
.AA 
HasKeyAA 
(AA 
uAA 
=>AA  "
uAA# $
.AA$ %
IdCiudadAA% -
)AA- .
;AA. /
entityBB 
.BB 
PropertyBB 
(BB  
uBB  !
=>BB" $
uBB% &
.BB& '
IdCiudadBB' /
)BB/ 0
.BB0 1
HasColumnNameBB1 >
(BB> ?
$strBB? J
)BBJ K
.BBK L
ValueGeneratedOnAddBBL _
(BB_ `
)BB` a
;BBa b
entityCC 
.CC 
PropertyCC 
(CC  
uCC  !
=>CC" $
uCC% &
.CC& '
NombreCC' -
)CC- .
.CC. /
HasColumnNameCC/ <
(CC< =
$strCC= E
)CCE F
.CCF G
HasMaxLengthCCG S
(CCS T
$numCCT W
)CCW X
;CCX Y
}DD 
)DD 
;DD 
modelBuilderFF 
.FF 
EntityFF 
<FF  
	LocalidadFF  )
>FF) *
(FF* +
entityFF+ 1
=>FF2 4
{GG 
entityHH 
.HH 
ToTableHH 
(HH 
$strHH *
)HH* +
;HH+ ,
entityII 
.II 
HasKeyII 
(II 
uII 
=>II  "
uII# $
.II$ %
IdLocalidadII% 0
)II0 1
;II1 2
entityJJ 
.JJ 
PropertyJJ 
(JJ  
uJJ  !
=>JJ" $
uJJ% &
.JJ& '
IdLocalidadJJ' 2
)JJ2 3
.JJ3 4
HasColumnNameJJ4 A
(JJA B
$strJJB P
)JJP Q
.JJQ R
ValueGeneratedOnAddJJR e
(JJe f
)JJf g
;JJg h
entityKK 
.KK 
PropertyKK 
(KK  
uKK  !
=>KK" $
uKK% &
.KK& '
NombreKK' -
)KK- .
.KK. /
HasColumnNameKK/ <
(KK< =
$strKK= E
)KKE F
.KKF G
HasMaxLengthKKG S
(KKS T
$numKKT W
)KKW X
;KKX Y
entityLL 
.LL 
PropertyLL 
(LL  
uLL  !
=>LL" $
uLL% &
.LL& '
IdCiudadLL' /
)LL/ 0
.LL0 1
HasColumnNameLL1 >
(LL> ?
$strLL? J
)LLJ K
;LLK L
entityNN 
.NN 
HasOneNN 
(NN 
uNN 
=>NN  "
uNN# $
.NN$ %
CiudadNN% +
)NN+ ,
.NN, -
WithManyNN- 5
(NN5 6
)NN6 7
.NN7 8
HasForeignKeyNN8 E
(NNE F
uNNF G
=>NNH J
uNNK L
.NNL M
IdCiudadNNM U
)NNU V
;NNV W
}OO 
)OO 
;OO 
modelBuilderRR 
.RR 
EntityRR 
<RR  
ModoTransaccionRR  /
>RR/ 0
(RR0 1
entityRR1 7
=>RR8 :
{SS 
entityTT 
.TT 
ToTableTT 
(TT 
$strTT 1
)TT1 2
;TT2 3
entityUU 
.UU 
HasKeyUU 
(UU 
uUU 
=>UU  "
uUU# $
.UU$ %
IdModoUU% +
)UU+ ,
;UU, -
entityVV 
.VV 
PropertyVV 
(VV  
uVV  !
=>VV" $
uVV% &
.VV& '
IdModoVV' -
)VV- .
.VV. /
HasColumnNameVV/ <
(VV< =
$strVV= F
)VVF G
.VVG H
ValueGeneratedOnAddVVH [
(VV[ \
)VV\ ]
;VV] ^
entityWW 
.WW 
PropertyWW 
(WW  
uWW  !
=>WW" $
uWW% &
.WW& '
NombreWW' -
)WW- .
.WW. /
HasColumnNameWW/ <
(WW< =
$strWW= E
)WWE F
.WWF G
HasMaxLengthWWG S
(WWS T
$numWWT W
)WWW X
;WWX Y
}XX 
)XX 
;XX 
modelBuilder[[ 
.[[ 
Entity[[ 
<[[  
TipoInmueble[[  ,
>[[, -
([[- .
entity[[. 4
=>[[5 7
{\\ 
entity]] 
.]] 
ToTable]] 
(]] 
$str]] /
)]]/ 0
;]]0 1
entity^^ 
.^^ 
HasKey^^ 
(^^ 
u^^ 
=>^^  "
u^^# $
.^^$ %
IdTipo^^% +
)^^+ ,
;^^, -
entity__ 
.__ 
Property__ 
(__  
u__  !
=>__" $
u__% &
.__& '
IdTipo__' -
)__- .
.__. /
HasColumnName__/ <
(__< =
$str__= F
)__F G
.__G H
ValueGeneratedOnAdd__H [
(__[ \
)__\ ]
;__] ^
entity`` 
.`` 
Property`` 
(``  
u``  !
=>``" $
u``% &
.``& '
Nombre``' -
)``- .
.``. /
HasColumnName``/ <
(``< =
$str``= E
)``E F
.``F G
HasMaxLength``G S
(``S T
$num``T V
)``V W
;``W X
}aa 
)aa 
;aa 
modelBuilderdd 
.dd 
Entitydd 
<dd  
EstadoPublicaciondd  1
>dd1 2
(dd2 3
entitydd3 9
=>dd: <
{ee 
entityff 
.ff 
ToTableff 
(ff 
$strff 4
)ff4 5
;ff5 6
entitygg 
.gg 
HasKeygg 
(gg 
ugg 
=>gg  "
ugg# $
.gg$ %
IdEstadogg% -
)gg- .
;gg. /
entityhh 
.hh 
Propertyhh 
(hh  
uhh  !
=>hh" $
uhh% &
.hh& '
IdEstadohh' /
)hh/ 0
.hh0 1
HasColumnNamehh1 >
(hh> ?
$strhh? J
)hhJ K
.hhK L
ValueGeneratedOnAddhhL _
(hh_ `
)hh` a
;hha b
entityii 
.ii 
Propertyii 
(ii  
uii  !
=>ii" $
uii% &
.ii& '
Nombreii' -
)ii- .
.ii. /
HasColumnNameii/ <
(ii< =
$strii= E
)iiE F
.iiF G
HasMaxLengthiiG S
(iiS T
$numiiT V
)iiV W
;iiW X
}jj 
)jj 
;jj 
modelBuildermm 
.mm 
Entitymm 
<mm  
Inmueblemm  (
>mm( )
(mm) *
entitymm* 0
=>mm1 3
{nn 
entityoo 
.oo 
ToTableoo 
(oo 
$stroo *
)oo* +
;oo+ ,
entitypp 
.pp 
HasKeypp 
(pp 
upp 
=>pp  "
upp# $
.pp$ %

IdInmueblepp% /
)pp/ 0
;pp0 1
entityrr 
.rr 
Propertyrr 
(rr  
urr  !
=>rr" $
urr% &
.rr& '

IdInmueblerr' 1
)rr1 2
.rr2 3
HasColumnNamerr3 @
(rr@ A
$strrrA N
)rrN O
.rrO P
ValueGeneratedOnAddrrP c
(rrc d
)rrd e
;rre f
entityss 
.ss 
Propertyss 
(ss  
uss  !
=>ss" $
uss% &
.ss& '
Tituloss' -
)ss- .
.ss. /
HasColumnNamess/ <
(ss< =
$strss= E
)ssE F
.ssF G
HasMaxLengthssG S
(ssS T
$numssT W
)ssW X
;ssX Y
entitytt 
.tt 
Propertytt 
(tt  
utt  !
=>tt" $
utt% &
.tt& '
Descripciontt' 2
)tt2 3
.tt3 4
HasColumnNamett4 A
(ttA B
$strttB O
)ttO P
.ttP Q
HasColumnTypettQ ^
(tt^ _
$strtt_ e
)tte f
;ttf g
entityuu 
.uu 
Propertyuu 
(uu  
uuu  !
=>uu" $
uuu% &
.uu& '
Preciouu' -
)uu- .
.uu. /
HasColumnNameuu/ <
(uu< =
$struu= E
)uuE F
.uuF G
HasPrecisionuuG S
(uuS T
$numuuT V
,uuV W
$numuuX Y
)uuY Z
;uuZ [
entityvv 
.vv 
Propertyvv 
(vv  
uvv  !
=>vv" $
uvv% &
.vv& '
IdTipovv' -
)vv- .
.vv. /
HasColumnNamevv/ <
(vv< =
$strvv= F
)vvF G
;vvG H
entityww 
.ww 
Propertyww 
(ww  
uww  !
=>ww" $
uww% &
.ww& '
IdModoww' -
)ww- .
.ww. /
HasColumnNameww/ <
(ww< =
$strww= F
)wwF G
;wwG H
entityxx 
.xx 
Propertyxx 
(xx  
uxx  !
=>xx" $
uxx% &
.xx& '
	Direccionxx' 0
)xx0 1
.xx1 2
HasColumnNamexx2 ?
(xx? @
$strxx@ K
)xxK L
.xxL M
HasMaxLengthxxM Y
(xxY Z
$numxxZ ]
)xx] ^
;xx^ _
entityyy 
.yy 
Propertyyy 
(yy  
uyy  !
=>yy" $
uyy% &
.yy& '
IdLocalidadyy' 2
)yy2 3
.yy3 4
HasColumnNameyy4 A
(yyA B
$stryyB P
)yyP Q
;yyQ R
entityzz 
.zz 
Propertyzz 
(zz  
uzz  !
=>zz" $
uzz% &
.zz& '
Habitacioneszz' 3
)zz3 4
.zz4 5
HasColumnNamezz5 B
(zzB C
$strzzC Q
)zzQ R
;zzR S
entity{{ 
.{{ 
Property{{ 
({{  
u{{  !
=>{{" $
u{{% &
.{{& '
Ba√±os{{' ,
){{, -
.{{- .
HasColumnName{{. ;
({{; <
$str{{< C
){{C D
;{{D E
entity|| 
.|| 
Property|| 
(||  
u||  !
=>||" $
u||% &
.||& '
MetrosCuadrados||' 6
)||6 7
.||7 8
HasColumnName||8 E
(||E F
$str||F X
)||X Y
;||Y Z
entity}} 
.}} 
Property}} 
(}}  
u}}  !
=>}}" $
u}}% &
.}}& '
Pisos}}' ,
)}}, -
.}}- .
HasColumnName}}. ;
(}}; <
$str}}< C
)}}C D
;}}D E
entity~~ 
.~~ 
Property~~ 
(~~  
u~~  !
=>~~" $
u~~% &
.~~& '
Estrato~~' .
)~~. /
.~~/ 0
HasColumnName~~0 =
(~~= >
$str~~> G
)~~G H
;~~H I
entity 
. 
Property 
(  
u  !
=>" $
u% &
.& '
Latitud' .
). /
./ 0
HasColumnName0 =
(= >
$str> G
)G H
.H I
HasPrecisionI U
(U V
$numV X
,X Y
$numZ [
)[ \
;\ ]
entity
ÄÄ 
.
ÄÄ 
Property
ÄÄ 
(
ÄÄ  
u
ÄÄ  !
=>
ÄÄ" $
u
ÄÄ% &
.
ÄÄ& '
Longitud
ÄÄ' /
)
ÄÄ/ 0
.
ÄÄ0 1
HasColumnName
ÄÄ1 >
(
ÄÄ> ?
$str
ÄÄ? I
)
ÄÄI J
.
ÄÄJ K
HasPrecision
ÄÄK W
(
ÄÄW X
$num
ÄÄX Z
,
ÄÄZ [
$num
ÄÄ\ ]
)
ÄÄ] ^
;
ÄÄ^ _
entity
ÅÅ 
.
ÅÅ 
Property
ÅÅ 
(
ÅÅ  
u
ÅÅ  !
=>
ÅÅ" $
u
ÅÅ% &
.
ÅÅ& '
	IdUsuario
ÅÅ' 0
)
ÅÅ0 1
.
ÅÅ1 2
HasColumnName
ÅÅ2 ?
(
ÅÅ? @
$str
ÅÅ@ L
)
ÅÅL M
;
ÅÅM N
entity
ÇÇ 
.
ÇÇ 
Property
ÇÇ 
(
ÇÇ  
u
ÇÇ  !
=>
ÇÇ" $
u
ÇÇ% &
.
ÇÇ& '
IdEstado
ÇÇ' /
)
ÇÇ/ 0
.
ÇÇ0 1
HasColumnName
ÇÇ1 >
(
ÇÇ> ?
$str
ÇÇ? J
)
ÇÇJ K
.
ÇÇK L
HasDefaultValue
ÇÇL [
(
ÇÇ[ \
$num
ÇÇ\ ]
)
ÇÇ] ^
;
ÇÇ^ _
entity
ÉÉ 
.
ÉÉ 
Property
ÉÉ 
(
ÉÉ  
u
ÉÉ  !
=>
ÉÉ" $
u
ÉÉ% &
.
ÉÉ& '
FechaPublicacion
ÉÉ' 7
)
ÉÉ7 8
.
ÉÉ8 9
HasColumnName
ÉÉ9 F
(
ÉÉF G
$str
ÉÉG Z
)
ÉÉZ [
.
ÉÉ[ \ 
HasDefaultValueSql
ÉÉ\ n
(
ÉÉn o
$strÉÉo Ç
)ÉÉÇ É
;ÉÉÉ Ñ
entity
ÖÖ 
.
ÖÖ 
HasOne
ÖÖ 
(
ÖÖ 
u
ÖÖ 
=>
ÖÖ  "
u
ÖÖ# $
.
ÖÖ$ %
TipoInmueble
ÖÖ% 1
)
ÖÖ1 2
.
ÖÖ2 3
WithMany
ÖÖ3 ;
(
ÖÖ; <
)
ÖÖ< =
.
ÖÖ= >
HasForeignKey
ÖÖ> K
(
ÖÖK L
u
ÖÖL M
=>
ÖÖN P
u
ÖÖQ R
.
ÖÖR S
IdTipo
ÖÖS Y
)
ÖÖY Z
;
ÖÖZ [
entity
ÜÜ 
.
ÜÜ 
HasOne
ÜÜ 
(
ÜÜ 
u
ÜÜ 
=>
ÜÜ  "
u
ÜÜ# $
.
ÜÜ$ %
ModoTransaccion
ÜÜ% 4
)
ÜÜ4 5
.
ÜÜ5 6
WithMany
ÜÜ6 >
(
ÜÜ> ?
)
ÜÜ? @
.
ÜÜ@ A
HasForeignKey
ÜÜA N
(
ÜÜN O
u
ÜÜO P
=>
ÜÜQ S
u
ÜÜT U
.
ÜÜU V
IdModo
ÜÜV \
)
ÜÜ\ ]
;
ÜÜ] ^
entity
áá 
.
áá 
HasOne
áá 
(
áá 
u
áá 
=>
áá  "
u
áá# $
.
áá$ %
	Localidad
áá% .
)
áá. /
.
áá/ 0
WithMany
áá0 8
(
áá8 9
)
áá9 :
.
áá: ;
HasForeignKey
áá; H
(
ááH I
u
ááI J
=>
ááK M
u
ááN O
.
ááO P
IdLocalidad
ááP [
)
áá[ \
;
áá\ ]
entity
àà 
.
àà 
HasOne
àà 
(
àà 
u
àà 
=>
àà  "
u
àà# $
.
àà$ %
Usuario
àà% ,
)
àà, -
.
àà- .
WithMany
àà. 6
(
àà6 7
)
àà7 8
.
àà8 9
HasForeignKey
àà9 F
(
ààF G
u
ààG H
=>
ààI K
u
ààL M
.
ààM N
	IdUsuario
ààN W
)
ààW X
;
ààX Y
entity
ââ 
.
ââ 
HasOne
ââ 
(
ââ 
u
ââ 
=>
ââ  "
u
ââ# $
.
ââ$ %
EstadoPublicacion
ââ% 6
)
ââ6 7
.
ââ7 8
WithMany
ââ8 @
(
ââ@ A
)
ââA B
.
ââB C
HasForeignKey
ââC P
(
ââP Q
u
ââQ R
=>
ââS U
u
ââV W
.
ââW X
IdEstado
ââX `
)
ââ` a
;
ââa b
}
ää 
)
ää 
;
ää 
modelBuilder
çç 
.
çç 
Entity
çç 
<
çç  
Imagen
çç  &
>
çç& '
(
çç' (
entity
çç( .
=>
çç/ 1
{
éé 
entity
èè 
.
èè 
ToTable
èè 
(
èè 
$str
èè )
)
èè) *
;
èè* +
entity
êê 
.
êê 
HasKey
êê 
(
êê 
u
êê 
=>
êê  "
u
êê# $
.
êê$ %
IdImagen
êê% -
)
êê- .
;
êê. /
entity
íí 
.
íí 
Property
íí 
(
íí  
u
íí  !
=>
íí" $
u
íí% &
.
íí& '
IdImagen
íí' /
)
íí/ 0
.
íí0 1
HasColumnName
íí1 >
(
íí> ?
$str
íí? J
)
ííJ K
.
ííK L!
ValueGeneratedOnAdd
ííL _
(
íí_ `
)
íí` a
;
íía b
entity
ìì 
.
ìì 
Property
ìì 
(
ìì  
u
ìì  !
=>
ìì" $
u
ìì% &
.
ìì& '
Url
ìì' *
)
ìì* +
.
ìì+ ,
HasColumnName
ìì, 9
(
ìì9 :
$str
ìì: ?
)
ìì? @
.
ìì@ A
HasMaxLength
ììA M
(
ììM N
$num
ììN Q
)
ììQ R
;
ììR S
entity
îî 
.
îî 
Property
îî 
(
îî  
u
îî  !
=>
îî" $
u
îî% &
.
îî& '

IdInmueble
îî' 1
)
îî1 2
.
îî2 3
HasColumnName
îî3 @
(
îî@ A
$str
îîA N
)
îîN O
;
îîO P
entity
ïï 
.
ïï 
Property
ïï 
(
ïï  
u
ïï  !
=>
ïï" $
u
ïï% &
.
ïï& '
Portada
ïï' .
)
ïï. /
.
ïï/ 0
HasColumnName
ïï0 =
(
ïï= >
$str
ïï> G
)
ïïG H
.
ïïH I
HasDefaultValue
ïïI X
(
ïïX Y
false
ïïY ^
)
ïï^ _
;
ïï_ `
entity
óó 
.
óó 
HasOne
óó 
(
óó 
u
óó 
=>
óó  "
u
óó# $
.
óó$ %
Inmueble
óó% -
)
óó- .
.
òò 
WithMany
òò 
(
òò 
i
òò 
=>
òò  "
i
òò# $
.
òò$ %
Imagenes
òò% -
)
òò- .
.
ôô 
HasForeignKey
ôô "
(
ôô" #
u
ôô# $
=>
ôô% '
u
ôô( )
.
ôô) *

IdInmueble
ôô* 4
)
ôô4 5
;
ôô5 6
}
öö 
)
öö 
;
öö 
modelBuilder
ùù 
.
ùù 
Entity
ùù 
<
ùù  
Caracteristica
ùù  .
>
ùù. /
(
ùù/ 0
entity
ùù0 6
=>
ùù7 9
{
ûû 
entity
üü 
.
üü 
ToTable
üü 
(
üü 
$str
üü 0
)
üü0 1
;
üü1 2
entity
†† 
.
†† 
HasKey
†† 
(
†† 
u
†† 
=>
††  "
u
††# $
.
††$ %
IdCaracteristica
††% 5
)
††5 6
;
††6 7
entity
°° 
.
°° 
Property
°° 
(
°°  
u
°°  !
=>
°°" $
u
°°% &
.
°°& '
IdCaracteristica
°°' 7
)
°°7 8
.
°°8 9
HasColumnName
°°9 F
(
°°F G
$str
°°G Z
)
°°Z [
.
°°[ \!
ValueGeneratedOnAdd
°°\ o
(
°°o p
)
°°p q
;
°°q r
entity
¢¢ 
.
¢¢ 
Property
¢¢ 
(
¢¢  
u
¢¢  !
=>
¢¢" $
u
¢¢% &
.
¢¢& '
Nombre
¢¢' -
)
¢¢- .
.
¢¢. /
HasColumnName
¢¢/ <
(
¢¢< =
$str
¢¢= E
)
¢¢E F
.
¢¢F G
HasMaxLength
¢¢G S
(
¢¢S T
$num
¢¢T W
)
¢¢W X
;
¢¢X Y
}
££ 
)
££ 
;
££ 
modelBuilder
¶¶ 
.
¶¶ 
Entity
¶¶ 
<
¶¶  $
InmuebleCaracteristica
¶¶  6
>
¶¶6 7
(
¶¶7 8
entity
¶¶8 >
=>
¶¶? A
{
ßß 
entity
®® 
.
®® 
ToTable
®® 
(
®® 
$str
®® 8
)
®®8 9
;
®®9 :
entity
©© 
.
©© 
HasKey
©© 
(
©© 
e
©© 
=>
©©  "
new
©©# &
{
©©' (
e
©©) *
.
©©* +

IdInmueble
©©+ 5
,
©©5 6
e
©©7 8
.
©©8 9
IdCaracteristica
©©9 I
}
©©J K
)
©©K L
;
©©L M
entity
´´ 
.
´´ 
Property
´´ 
(
´´  
u
´´  !
=>
´´" $
u
´´% &
.
´´& '

IdInmueble
´´' 1
)
´´1 2
.
´´2 3
HasColumnName
´´3 @
(
´´@ A
$str
´´A N
)
´´N O
;
´´O P
entity
¨¨ 
.
¨¨ 
Property
¨¨ 
(
¨¨  
u
¨¨  !
=>
¨¨" $
u
¨¨% &
.
¨¨& '
IdCaracteristica
¨¨' 7
)
¨¨7 8
.
¨¨8 9
HasColumnName
¨¨9 F
(
¨¨F G
$str
¨¨G Z
)
¨¨Z [
;
¨¨[ \
entity
ÆÆ 
.
ÆÆ 
HasOne
ÆÆ 
(
ÆÆ 
u
ÆÆ 
=>
ÆÆ  "
u
ÆÆ# $
.
ÆÆ$ %
Inmueble
ÆÆ% -
)
ÆÆ- .
.
ØØ 
WithMany
ØØ 
(
ØØ  
i
ØØ  !
=>
ØØ" $
i
ØØ% &
.
ØØ& '%
InmuebleCaracteristicas
ØØ' >
)
ØØ> ?
.
∞∞ 
HasForeignKey
∞∞ $
(
∞∞$ %
u
∞∞% &
=>
∞∞' )
u
∞∞* +
.
∞∞+ ,

IdInmueble
∞∞, 6
)
∞∞6 7
;
∞∞7 8
entity
±± 
.
±± 
HasOne
±± 
(
±± 
u
±± 
=>
±±  "
u
±±# $
.
±±$ %
Caracteristica
±±% 3
)
±±3 4
.
±±4 5
WithMany
±±5 =
(
±±= >
)
±±> ?
.
±±? @
HasForeignKey
±±@ M
(
±±M N
u
±±N O
=>
±±P R
u
±±S T
.
±±T U
IdCaracteristica
±±U e
)
±±e f
;
±±f g
}
≤≤ 
)
≤≤ 
;
≤≤ 
modelBuilder
µµ 
.
µµ 
Entity
µµ 
<
µµ  
Contacto
µµ  (
>
µµ( )
(
µµ) *
entity
µµ* 0
=>
µµ1 3
{
∂∂ 
entity
∑∑ 
.
∑∑ 
ToTable
∑∑ 
(
∑∑ 
$str
∑∑ *
)
∑∑* +
;
∑∑+ ,
entity
∏∏ 
.
∏∏ 
HasKey
∏∏ 
(
∏∏ 
u
∏∏ 
=>
∏∏  "
u
∏∏# $
.
∏∏$ %

IdContacto
∏∏% /
)
∏∏/ 0
;
∏∏0 1
entity
∫∫ 
.
∫∫ 
Property
∫∫ 
(
∫∫  
u
∫∫  !
=>
∫∫" $
u
∫∫% &
.
∫∫& '

IdContacto
∫∫' 1
)
∫∫1 2
.
∫∫2 3
HasColumnName
∫∫3 @
(
∫∫@ A
$str
∫∫A N
)
∫∫N O
.
∫∫O P!
ValueGeneratedOnAdd
∫∫P c
(
∫∫c d
)
∫∫d e
;
∫∫e f
entity
ªª 
.
ªª 
Property
ªª 
(
ªª  
u
ªª  !
=>
ªª" $
u
ªª% &
.
ªª& '
IdComprador
ªª' 2
)
ªª2 3
.
ªª3 4
HasColumnName
ªª4 A
(
ªªA B
$str
ªªB P
)
ªªP Q
;
ªªQ R
entity
ºº 
.
ºº 
Property
ºº 
(
ºº  
u
ºº  !
=>
ºº" $
u
ºº% &
.
ºº& '

IdVendedor
ºº' 1
)
ºº1 2
.
ºº2 3
HasColumnName
ºº3 @
(
ºº@ A
$str
ººA N
)
ººN O
;
ººO P
entity
ΩΩ 
.
ΩΩ 
Property
ΩΩ 
(
ΩΩ  
u
ΩΩ  !
=>
ΩΩ" $
u
ΩΩ% &
.
ΩΩ& '

IdInmueble
ΩΩ' 1
)
ΩΩ1 2
.
ΩΩ2 3
HasColumnName
ΩΩ3 @
(
ΩΩ@ A
$str
ΩΩA N
)
ΩΩN O
;
ΩΩO P
entity
ææ 
.
ææ 
Property
ææ 
(
ææ  
u
ææ  !
=>
ææ" $
u
ææ% &
.
ææ& '
Mensaje
ææ' .
)
ææ. /
.
ææ/ 0
HasColumnName
ææ0 =
(
ææ= >
$str
ææ> G
)
ææG H
.
ææH I
HasColumnType
ææI V
(
ææV W
$str
ææW ]
)
ææ] ^
;
ææ^ _
entity
øø 
.
øø 
Property
øø 
(
øø  
u
øø  !
=>
øø" $
u
øø% &
.
øø& '
Fecha
øø' ,
)
øø, -
.
øø- .
HasColumnName
øø. ;
(
øø; <
$str
øø< C
)
øøC D
.
øøD E 
HasDefaultValueSql
øøE W
(
øøW X
$str
øøX k
)
øøk l
;
øøl m
entity
¡¡ 
.
¡¡ 
HasOne
¡¡ 
(
¡¡ 
u
¡¡ 
=>
¡¡  "
u
¡¡# $
.
¡¡$ %
	Comprador
¡¡% .
)
¡¡. /
.
¡¡/ 0
WithMany
¡¡0 8
(
¡¡8 9
)
¡¡9 :
.
¡¡: ;
HasForeignKey
¡¡; H
(
¡¡H I
u
¡¡I J
=>
¡¡K M
u
¡¡N O
.
¡¡O P
IdComprador
¡¡P [
)
¡¡[ \
.
¡¡\ ]
OnDelete
¡¡] e
(
¡¡e f
DeleteBehavior
¡¡f t
.
¡¡t u
Restrict
¡¡u }
)
¡¡} ~
;
¡¡~ 
entity
¬¬ 
.
¬¬ 
HasOne
¬¬ 
(
¬¬ 
u
¬¬ 
=>
¬¬  "
u
¬¬# $
.
¬¬$ %
Vendedor
¬¬% -
)
¬¬- .
.
¬¬. /
WithMany
¬¬/ 7
(
¬¬7 8
)
¬¬8 9
.
¬¬9 :
HasForeignKey
¬¬: G
(
¬¬G H
u
¬¬H I
=>
¬¬J L
u
¬¬M N
.
¬¬N O

IdVendedor
¬¬O Y
)
¬¬Y Z
.
¬¬Z [
OnDelete
¬¬[ c
(
¬¬c d
DeleteBehavior
¬¬d r
.
¬¬r s
Restrict
¬¬s {
)
¬¬{ |
;
¬¬| }
entity
√√ 
.
√√ 
HasOne
√√ 
(
√√ 
u
√√ 
=>
√√  "
u
√√# $
.
√√$ %
Inmueble
√√% -
)
√√- .
.
√√. /
WithMany
√√/ 7
(
√√7 8
)
√√8 9
.
√√9 :
HasForeignKey
√√: G
(
√√G H
u
√√H I
=>
√√J L
u
√√M N
.
√√N O

IdInmueble
√√O Y
)
√√Y Z
;
√√Z [
}
ƒƒ 
)
ƒƒ 
;
ƒƒ 
}
≈≈ 	
}
∆∆ 
}«« Ø#
eC:\Users\Kevin\source\repos\Controllers\github\Backend\Rever\rever\AdministradoresInjectionService.cs
	namespace 	
rever
 
{ 
public 

static 
class +
AdministradoresInjectionService 7
{ 
public 
static 
IServiceCollection (
AddExternal) 4
(4 5
this5 9
IServiceCollection: L
servicesM U
,U V
IConfigurationW e
configurationf s
)s t
{ 	
String 
connectionString #
=$ %
$str& (
;( )
connectionString 
= 
configuration ,
[, -
$str- U
]U V
;V W
services 
. 
AddDbContext !
<! "
DatabaseService" 1
>1 2
(2 3
options3 :
=>; =
options 
. 
UseSqlServer $
($ %
connectionString% 5
,5 6

sqlOptions7 A
=>B D

sqlOptions 
.  
EnableRetryOnFailure 3
(3 4
maxRetryCount %
:% &
$num' (
,( )
maxRetryDelay %
:% &
TimeSpan' /
./ 0
FromSeconds0 ;
(; <
$num< >
)> ?
,? @
errorNumbersToAdd )
:) *
null+ /
)/ 0
)0 1
)1 2
;2 3
services 
. 
	AddScoped 
< 
IUsuarioRepository 1
,1 2
UsuarioRepository3 D
>D E
(E F
)F G
;G H
services 
. 
	AddScoped 
< 
ICiudadRepository 0
,0 1
CiudadRepository2 B
>B C
(C D
)D E
;E F
services 
. 
	AddScoped 
<  
ILocalidadRepository 3
,3 4
LocalidadRepository5 H
>H I
(I J
)J K
;K L
services 
. 
	AddScoped 
< &
IModoTransaccionRepository 9
,9 :%
ModoTransaccionRepository; T
>T U
(U V
)V W
;W X
services 
. 
	AddScoped 
< -
!IInmuebleCaracteristicaRepository @
,@ A,
 InmuebleCaracteristicaRepositoryB b
>b c
(c d
)d e
;e f
services   
.   
	AddScoped   
<   #
ITipoInmuebleRepository   6
,  6 7"
TipoInmuebleRepository  8 N
>  N O
(  O P
)  P Q
;  Q R
services!! 
.!! 
	AddScoped!! 
<!! (
IEstadoPublicacionRepository!! ;
,!!; <'
EstadoPublicacionRepository!!= X
>!!X Y
(!!Y Z
)!!Z [
;!![ \
services"" 
."" 
	AddScoped"" 
<"" 
IInmuebleRepository"" 2
,""2 3
InmuebleRepository""4 F
>""F G
(""G H
)""H I
;""I J
services## 
.## 
	AddScoped## 
<## 
IImagenRepository## 0
,##0 1
ImagenRepository##2 B
>##B C
(##C D
)##D E
;##E F
services$$ 
.$$ 
	AddScoped$$ 
<$$ %
ICaracteristicaRepository$$ 8
,$$8 9$
CaracteristicaRepository$$: R
>$$R S
($$S T
)$$T U
;$$U V
services%% 
.%% 
	AddScoped%% 
<%% 
IContactoRepository%% 2
,%%2 3
ContactoRepository%%4 F
>%%F G
(%%G H
)%%H I
;%%I J
services&& 
.&& 
	AddScoped&& 
<&& 
IRolRepository&& -
,&&- .
RolRepository&&/ <
>&&< =
(&&= >
)&&> ?
;&&? @
services'' 
.'' 
	AddScoped'' 
<'' 
IEmailService'' ,
,'', -
EmailService''. :
>'': ;
(''; <
)''< =
;''= >
return)) 
services)) 
;)) 
}** 	
}++ 
},, 