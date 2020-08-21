import * as signalR from "@aspnet/signalr";
import config from "react-global-configuration";

class SignalrManager {
    public connection: signalR.HubConnection;

    constructor() {
        const backendURL = config.get("urlApi");

        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(`${backendURL}StreamHub`)
            .build();

        this.connection.on("updateCount", (number: string) => {
            const counterBlock: any = document.getElementById("userCounter");

            counterBlock.innerHTML = number;
        });
    }

    public async connectToHub(): Promise<void> {
        console.log("Try to connect to SignalR Hub");

        return await this.connection.start();
    }

    public async disconnectFromHub(): Promise<void> {
        console.log("Try to disconnect from SignalR Hub");

        return await this.connection.stop();
    }

    public async sendCommand(command: string): Promise<void> {
        console.log("Try send to SignalR Hub:" + command);

        return await this.connection.send("SendMessage", command);
    }
}

export default SignalrManager;
