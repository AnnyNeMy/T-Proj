import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FavouriteBondComponent } from './favourite-bond.component';

describe('FavouriteBondComponent', () => {
  let component: FavouriteBondComponent;
  let fixture: ComponentFixture<FavouriteBondComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FavouriteBondComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FavouriteBondComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
