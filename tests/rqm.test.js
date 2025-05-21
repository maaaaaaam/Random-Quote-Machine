import { getQuote } from '../src/functions.js'

describe('getQuote', () => {
    it('should return an object with the text and the author of a quote', () => {expect(getQuote()).toBeTruthy()})
})