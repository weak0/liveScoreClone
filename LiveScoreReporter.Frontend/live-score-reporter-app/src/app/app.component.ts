import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'live-score-reporter-app';
 
  constructor(private router: Router) {}

  goHome(): void {
    this.router.navigate(['/']);
  }
}
