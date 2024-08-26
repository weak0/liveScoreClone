import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MatchListComponent } from './match-list/match-list.component';
import { MatchDetailComponent } from './match-detail/match-detail.component';

const routes: Routes = [
  { path: '', component: MatchListComponent },
  { path: 'match/:id', component: MatchDetailComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
