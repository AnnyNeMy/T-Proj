export class CommonRequest
{
    public Id: string = "";
    constructor(data: { Id: string }) {
        this.Id = data.Id;
      }
}