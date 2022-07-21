# unity-poster-webgl
 
## Soporte Big Sur (Mac) / Chrome última versión
Cada vez que se genere el proyecto, editar el archivo Build/WEBGL.loader.js.

1. Formatear el archivo con Shift + Alt + F (Windows), con cualquier formateador de javascript (yo utilicé el IDE VsCode).
2. Reemplazar esta línea 
```
    h = /Mac OS X (10[\.\_\d]+)/.exec(s)[1]
``` 
   por:
```
    h = /Mac OS X (1[\.\_\d][\.\_\d]+)/.exec(s)[1];
```