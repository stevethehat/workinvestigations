class Box{
    constructor(canvas, definition){
        this.canvas = canvas;
        this.anchors = {};
        if(definition){
            this.draw(definition);
        }

    }

    setupAnchors(){
        var definition = this.definition;
        this.anchors = {
            "l": { x:definition.x , y:definition.y + (definition.height / 2)},
            "tl": { x:definition.x + (definition.width * 0.25) , y:definition.y },
            "tc": { x:definition.x + (definition.width * 0.5) , y:definition.y },
            "tr": { x:definition.x + (definition.width * 0.75) , y:definition.y },
            "r": { x:definition.x + definition.width , y:definition.y + (definition.height / 2) }, 
            "br": { x:definition.x + (definition.width * 0.25) , y:definition.y + definition.height },
            "bc": { x:definition.x + (definition.width * 0.5) , y:definition.y + definition.height },
            "bl": { x:definition.x + (definition.width * 0.75) , y:definition.y + definition.height }
        }
    }

    dot(position){
        const context = this.canvas.context;
        const anchor = this.anchors[position];

        context.beginPath();
        context.arc(anchor.x, anchor.y, 1, 0, 2 * Math.PI, true);
        context.stroke();
    }

    draw(definition){
        this.definition = definition;
        const context = this.canvas.context;

        this.setupAnchors();

        context.rect(definition.x, definition.y, definition.width, definition.height);
        context.lineWidth = definition.lineWidth;
        context.strokeStyle = definition.lineColor;
        context.stroke();

        context.font = "16pt Arial";
        context.fillText(definition.title, definition.x + 4, definition.y + 24);

        this.dot("l");
        this.dot("tl");
        this.dot("tc");
        this.dot("tr");
        this.dot("r");
        this.dot("br");
        this.dot("bc");
        this.dot("bl");
    }
}

export default Box;