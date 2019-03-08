export class CPQWholegood{
    public Id               : number;
    public StockNumber      : string;
    public Make             : string;
    public Model            : string;
    public Price            : number;
    public ListPrice        : number;
    public DealerDiscount   : number;

    constructor() {
        this.Id             = 0;
        this.StockNumber    = '10001005';
        this.Make           = 'VA';
        this.Model          = 'S274';
        this.Price          = 110000;
        this.ListPrice      = 100000;
        this.DealerDiscount = 2000;
    }

    getCPQData(): any{
        const result = {
            configrationId  : this.StockNumber,
            brand           : this.Make,
            model           : this.Model,
            totalPrice      : this.Price,
            listprice       : this.ListPrice,
            dealerDiscount  : this.DealerDiscount
        }

        return result;
    }
}

export class CPQTradeIn{
    public Id                   : number;
    public SerialNumber         : string;
    public Make                 : string;
    public Model                : string;
    public Description          : string;
    public Condition            : string;
    public CompensationValue    : number;
    public BookValue            : number;
    
    constructor() {
        this.Id                 = 0;
        this.SerialNumber       = '';
        this.Make               = '';
        this.Model              = '';
        this.Description        = '';
        this.Condition          = '';
        this.CompensationValue  = 0;
        this.BookValue          = 0;
    }

    getCPQData(){
        const result = {
            serialNumber: this.SerialNumber,
            brand: this.Make,
            model: this.Model,
            modelVariant: this.Description,
            compensationValue : this.CompensationValue,
            bookValue: this.BookValue,
            machineCondition: this.Condition,
        }   
        return result; 
    }
}

//Comment = $"{attachment.Name} - {attachment.Description} [{attachment.ConfigurationId}] {attachment.Price:c}".Replace("£", ""),
export class CPQAdditionalItem{
    public Id               : number;
    public Name             : string;
    public Description      : string;
    public PartId           : string;
    public Price            : number;

    constructor(){
        this.Id             = 0;
        this.Name           = '';
        this.Description    = '';
        this.PartId         = '';
        this.Price          = 0;
    }

    getCPQData(){
        const result = {
            name            : this.Name,
            description     : this.Description,
            configurationId : this.PartId,
            price           : this.Price
        }   
        return result; 
    }
}