export class Canvas {
    public context: CanvasRenderingContext2D | null = null;
    public displayCanvas: HTMLCanvasElement;
    /**
     */
    constructor(name: string) {
        debugger;
        this.displayCanvas = document.getElementById(name) as HTMLCanvasElement;
        if (undefined !== this.displayCanvas && null !== this.displayCanvas) {
            this.displayCanvas.width = this.displayCanvas.getBoundingClientRect().width;
            this.displayCanvas.height = this.displayCanvas.getBoundingClientRect().height;
            this.context = this.displayCanvas.getContext('2d') as CanvasRenderingContext2D;
        }
    }
    clear() {
        if (null !== this.context) {
            this.context.clearRect(0, 0, this.displayCanvas.width, this.displayCanvas.height);
        }
    }
}

export interface IPoint {
    x: number;
    y: number;
}

