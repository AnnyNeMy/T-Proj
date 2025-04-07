import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PositionDealsComponent } from './position-deals.component';

describe('PositionDealsComponent', () => {
  let component: PositionDealsComponent;
  let fixture: ComponentFixture<PositionDealsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PositionDealsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PositionDealsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
