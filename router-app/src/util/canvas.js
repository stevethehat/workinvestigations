class Canvas{
    constructor(name){
        this.displayCanvas = document.getElementById(name);
        this.displayCanvas.width = this.displayCanvas.getBoundingClientRect().width;
        this.displayCanvas.height = this.displayCanvas.getBoundingClientRect().height;

        this.context = this.displayCanvas.getContext("2d");
    }
}

export default Canvas;