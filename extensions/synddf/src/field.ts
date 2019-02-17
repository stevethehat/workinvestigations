import { Base } from "./base";
import { Model } from "./model";

export class Field extends Base{
    //ModelName: string;
    Model: Model;
    constructor(name: string, modelName: string) {
        super(name);
        this.Model = new Model(modelName);
    }
    getHover(): string{
        return '';
    }
    getFileName(): string {
        return this.Model.FileName;   
    }
}