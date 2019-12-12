import { Component } from '@angular/core';
import { DataService } from '../../shared/dataService';
import { Router } from '@angular/router';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
/** login component*/
export class LoginComponent {

    errorMessage: string;

/** login ctor */
    constructor(private data: DataService, private router: Router) {

    }

    public creds = { username: "", password: "" };

    onLogin() {
        this.data.login(this.creds).subscribe(
            success => {
                if (success) {
                    if (this.data.order.items.length == 0) { this.router.navigate([""]); }
                    else { this.router.navigate(["checkout"]); }
                }                
            },
            error => { this.errorMessage = "Failed to login" });
    }
}