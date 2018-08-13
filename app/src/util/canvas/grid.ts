import {Canvas} from '@/util/canvas/canvas';

export default class Grid {
    public Width: number;
    public Height: number;
    public VSlots: number[];
    public HSlots: number[];
    protected Canvas: Canvas;
    protected Vertical: number;
    protected Horrizontal: number;
    protected HMargin: number;
    protected VMargin: number;

    constructor(
        canvas: Canvas,
        horrizontal: number, vertical: number,
        width: number, height: number,
        hMargin: number, vMargin: number) {

        this.Canvas = canvas;
        this.Vertical = vertical;
        this.Horrizontal = horrizontal;
        this.Width = width;
        this.Height = height;
        this.HMargin = hMargin;
        this.VMargin = vMargin;
        this.VSlots = [];
        this.HSlots = [];

        this.setupHSlots();
        this.setupVSlots();
    }

    public setupHSlots() {
        const totalWidth = (this.Horrizontal * this.Width) + ((this.Horrizontal - 2) * this.HMargin);
        const margins = (this.Canvas.DisplayCanvas.width - totalWidth) / 2;

        let x = margins;
        for (let i = 0; i < this.Horrizontal; i++) {
            this.HSlots[i] = x;
            x = x + this.Width + this.HMargin;
        }
    }

    public setupVSlots() {
        // debugger;
        const totalHeight = (this.Vertical * this.Height) + ((this.Vertical - 1) * this.VMargin);
        const margins = (this.Canvas.DisplayCanvas.height - totalHeight) / 2;

        let y = margins;
        for (let i = 0; i < this.Vertical; i++) {
            this.VSlots[i] = y;
            y = y + this.Height + this.VMargin;
        }
    }
}
