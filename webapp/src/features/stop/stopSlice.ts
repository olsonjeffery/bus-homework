import { createAsyncThunk, createSlice, createAction, PayloadAction, Action } from '@reduxjs/toolkit';
import { Root } from 'react-dom/client';
import { RootState, AppThunk, store } from '../../app/store';
import { StopEndpointResult } from './data';

export interface StopState {
  visibleStops: Array<StopEndpointResult>,
}

export const initialState: StopState = {
  visibleStops: [],
};

export const stopSlice = createSlice({
  name: 'stop',
  initialState,
  reducers: {
    setVisibleStops: (state, action: PayloadAction<StopEndpointResult[]>) => {
      state.visibleStops = action.payload;
    },
  },
});

export const selectVisibleStops = (state: RootState) => state.stop.visibleStops;

export const { setVisibleStops } = stopSlice.actions;

export default stopSlice.reducer;