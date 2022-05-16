import { createAsyncThunk, createSlice, createAction, PayloadAction, Action } from '@reduxjs/toolkit';
import { Root } from 'react-dom/client';
import { Bool } from 'reselect/es/types';
import { RootState, AppThunk, store } from '../../app/store';
import { StopEndpointResult } from './data';
import { DateTime } from 'luxon';

export interface StopState {
  visibleStops: Array<StopEndpointResult>,
  lastChecked: string
  hasSetIntervalHappened: Boolean
}

export const initialState: StopState = {
  visibleStops: [],
  lastChecked: "",
  hasSetIntervalHappened: false
};

export const stopSlice = createSlice({
  name: 'stop',
  initialState,
  reducers: {
    setVisibleStops: (state, action: PayloadAction<StopEndpointResult[]>) => {
      state.visibleStops = action.payload;
      state.lastChecked = DateTime.now().toFormat('f')
    },
    setLastChecked: (state, action: PayloadAction<string>) => {
      state.lastChecked = action.payload;
    },
    setHasSetIntervalHappened: (state, action: PayloadAction<Boolean>) => {
      state.hasSetIntervalHappened = action.payload
    }
  },
});

export const selectVisibleStops = (state: RootState) => state.stop.visibleStops;
export const selectLastChecked = (state: RootState) => state.stop.lastChecked;
export const selectHasSetIntervalHappened = (state: RootState) => state.stop.hasSetIntervalHappened;

export const { setVisibleStops, setLastChecked, setHasSetIntervalHappened } = stopSlice.actions;

export default stopSlice.reducer;